using CarShop.Models;
using CarShop.Database;
using Npgsql;
namespace CarShop.Services;


public class CarService
{
    private readonly BbConnection _db;

    public CarService(BbConnection db)
    {
        _db = db;
    }
    private readonly List<Car> _cars = new();
    private int _nextId = 1;

    public void ShowCars()
    {
        using var conn = _db.GetConnection();
        // isEmpty check SQL
        using var sql = new NpgsqlCommand("SELECT brand, model, price, available FROM cars", conn);
        using var res = sql.ExecuteReader();
        if (!res.HasRows) 
        {
            Console.WriteLine("❌ В наличии нет машин.");
            return;
        }

        Console.WriteLine("\n🚗 Список машин:");
           
           while (res.Read())
           {
               bool available = (bool)res["available"];
               if(available)
               {
                   Console.WriteLine($"{res["brand"]} {res["model"]}- {res["price"]} kzt");
               }
           }
          
    }

    public void AddCar()
    {
        using var conn = _db.GetConnection();
        Console.Write("Введите марка машины: ");
        string brand = Console.ReadLine()!;
        Console.Write("Введите модель машины: ");
        string model = Console.ReadLine()!;
        Console.Write("Введите цену машины: ");
        decimal price;
        
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out price) && price >= 0)
                break;
            Console.Write("Неверная цена. Введите положительное число: ");
        }
        using var sql2 = new NpgsqlCommand("INSERT INTO cars (brand,model,price,available) VALUES (@b,@m,@p,true)", conn);
        sql2.Parameters.AddWithValue("@b", brand);
        sql2.Parameters.AddWithValue("@m", model);
        sql2.Parameters.AddWithValue("@p", price);
        sql2.ExecuteNonQuery();

        //var car = new Car(_nextId++, brand, model, price);
       // _cars.Add(car);
        //Console.WriteLine("✅ Машина добавлена!");
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
        using var conn = _db.GetConnection();
        
            //_cars.Remove(car);
            //Thread.Sleep(6000);
            using var sql3 = new NpgsqlCommand("DELETE FROM cars WHERE id = @id", conn);
            sql3.Parameters.AddWithValue("@id", id);
            sql3.ExecuteNonQuery();
            Console.WriteLine("✅ Машина удалена.");
       
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


        //_cars.Remove(car);
        using  var conn = _db.GetConnection();
        using var sql4 = new NpgsqlCommand("UPDATE cars SET available = false WHERE car_id = @id", conn);
        sql4.Parameters.AddWithValue("@id", id);
        sql4.ExecuteNonQuery();
        Console.WriteLine($"✅ Поздравляем с покупкой ! Ваш баланс: {user.Balance} KZT");


    }
}