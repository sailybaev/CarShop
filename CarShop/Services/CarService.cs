using CarShop.Models;
using CarShop.Database;
using Npgsql;

namespace CarShop.Services;

public class CarService
{
    private readonly BbConnection _bbConnection;

    public CarService(BbConnection bbConnection)
    {
        _bbConnection = bbConnection;
    }

    public void ShowCars() // Pokazat' spisok mashin
    {
        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand("SELECT id, name, price FROM cars WHERE owner_username IS NULL ORDER BY id", conn);
        using var reader = cmd.ExecuteReader();

        var any = false;
        Console.WriteLine();
        while (reader.Read())
        {
            any = true;
            var id = reader.GetInt32(reader.GetOrdinal("id"));
            var name = reader.GetString(reader.GetOrdinal("name"));
            var price = reader.GetDecimal(reader.GetOrdinal("price"));
            var car = new Car(id, name, price);
            Console.WriteLine(car);
        }

        if (!any)
        {
            Console.WriteLine("Нет доступных машин.");
        }
    }

    public void AddCar()
    {
        Console.Write("Введите название машины: ");
        string name = Console.ReadLine()!;
        Console.Write("Введите цену машины: ");

        decimal price;
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out price) && price >= 0)
                break;
            Console.Write("Неверная цена. Введите положительное число: ");
        }

        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand("INSERT INTO cars (name, price) VALUES (@n, @p) RETURNING id", conn);
        cmd.Parameters.AddWithValue("@n", name);
        cmd.Parameters.AddWithValue("@p", price);

        try
        {
            var id = (int)cmd.ExecuteScalar()!;
            Console.WriteLine($"✅ Машина добавлена с ID {id}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при добавлении машины: {ex.Message}");
        }
    }

    public void RemoveCar()
    {
        Console.Write("Введите ID машины для удаления: ");

        int id;
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out id) && id > 0)
                break;
            Console.Write("Неверный ID. Введите положительное целое число: ");
        }

        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand("DELETE FROM cars WHERE id = @id AND owner_username IS NULL", conn);
        cmd.Parameters.AddWithValue("@id", id);

        var affected = cmd.ExecuteNonQuery();
        if (affected > 0)
        {
            Console.WriteLine("✅ Машина удалена.");
        }
        else
        {
            Console.WriteLine("❌ Машина не найдена или уже продана.");
        }
    }

    public void BuyCar(User user)
    {
        ShowCars();
        Console.Write("Введите ID машины для покупки: ");

        int id;
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out id) && id > 0)
                break;
            Console.Write("Неверный ID. Введите положительное целое число: ");
        }

        using var conn = _bbConnection.GetConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            using var cmd = new NpgsqlCommand("SELECT price, owner_username, name FROM cars WHERE id = @id FOR UPDATE", conn, tran);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                Console.WriteLine("❌ Машина не найдена.");
                tran.Rollback();
                return;
            }

            var price = reader.GetDecimal(reader.GetOrdinal("price"));
            var name = reader.GetString(reader.GetOrdinal("name"));
            var ownerOrdinal = reader.GetOrdinal("owner_username");
            string? owner = reader.IsDBNull(ownerOrdinal) ? null : reader.GetString(ownerOrdinal);
            reader.Close();

            if (owner != null)
            {
                Console.WriteLine("❌ Машина уже продана.");
                tran.Rollback();
                return;
            }

            if (user.Balance < price)
            {
                Console.WriteLine("❌ Недостаточно средств!");
                tran.Rollback();
                return;
            }

            // Deduct balance
            using (var updateUser = new NpgsqlCommand("UPDATE users SET balance = balance - @price WHERE username = @u", conn, tran))
            {
                updateUser.Parameters.AddWithValue("@price", price);
                updateUser.Parameters.AddWithValue("@u", user.Username);
                var rows = updateUser.ExecuteNonQuery();
                if (rows == 0)
                {
                    Console.WriteLine("❌ Пользователь не найден.");
                    tran.Rollback();
                    return;
                }
            }

            // Assign car to user
            using (var updateCar = new NpgsqlCommand("UPDATE cars SET owner_username = @u WHERE id = @id", conn, tran))
            {
                updateCar.Parameters.AddWithValue("@u", user.Username);
                updateCar.Parameters.AddWithValue("@id", id);
                updateCar.ExecuteNonQuery();
            }

            tran.Commit();

            user.Balance -= price;
            user.BoughtCars.Add(new Car(id, name, price));

            Console.WriteLine($"✅ Поздравляем с покупкой {name}! Ваш баланс: {user.Balance} KZT");
        }
        catch (Exception ex)
        {
            try { tran.Rollback(); } catch (Exception rbEx) { Console.WriteLine($"Rollback failed: {rbEx.Message}"); }
            Console.WriteLine($"❌ Ошибка при покупке: {ex.Message}");
        }
    }
}