using CarShop.Enums;
using CarShop.Models;
using Npgsql;
using CarShop.Database;

namespace CarShop.Services;


public class AuthService
{
    private readonly BbConnection _db;

    public AuthService(BbConnection db)
    {
        _db = db;
    }

    public AuthService()
    {
    }

    private readonly List<User> _users = new();
    public User? LoggedInUser { get; private set; }

    public void Register()
    {
        Console.Write("Введите имя пользователя: ");
        string username = Console.ReadLine()!;
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine()!;
        Console.Write("Выберите роль (1 — Клиент, 2 — Админ): ");

        int roleChoice;
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                roleChoice = 1;
                break;
            }

            if (int.TryParse(input, out roleChoice) && (roleChoice == 1 || roleChoice == 2))
                break;

            Console.Write("Неверный ввод. Введите 1 для Клиента или 2 для Админа: ");
        }

        UserRole role = roleChoice == 2 ? UserRole.Admin : UserRole.Client;

        //_users.Add(new User(username, password, role, 100000)); // Начальный баланс 100000 для клиентов
        using var conn = _db.GetConnection();
        using var sql2 =
            new NpgsqlCommand(
                "INSERT INTO users(username,password,role,balance ) VALUES(@name,@password,@role,@balans)", conn);
        sql2.Parameters.AddWithValue("@name", username);
        sql2.Parameters.AddWithValue("@password", password);
        sql2.Parameters.AddWithValue("@role", roleChoice);
        sql2.Parameters.AddWithValue("@balans", 100000);
        sql2.ExecuteNonQuery();
        Console.WriteLine("✅ Пользователь зарегистрирован!");
    }

    public void Login()
    {
        Console.Write("Имя пользователя: ");
        string username = Console.ReadLine()!;
        Console.Write("Пароль: ");
        string password = Console.ReadLine()!;

        try
        {
            using var conn = _db.GetConnection();
            using var sql =
                new NpgsqlCommand(
                    "SELECT id,username, password, role, balance FROM users WHERE username = @u AND password = @p LIMIT 1",
                    conn);
            sql.Parameters.AddWithValue("@u", username);
            sql.Parameters.AddWithValue("@p", password);

            using var reader = sql.ExecuteReader();
            if (!reader.Read())
            {
                Console.WriteLine("Неверные данные для входа!");
                return;
            }

            int index = reader.GetInt32(0);
            string usern = reader.GetString(1);
            string role = reader.GetString(3);
            decimal balance = reader.GetDecimal(4);

            UserRole rl = new UserRole();

            if (role == "1")
                rl = UserRole.Client;
            else if (role == "2")
                rl = UserRole.Admin;

            LoggedInUser = new User(index++, usern, password, rl, balance);
            Console.WriteLine($"Добро пожаловать, {LoggedInUser.Username} ({LoggedInUser.Role})!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Ошибка при входе: {ex.Message}");
        }
    }

    public void Logout()
    {
        if (LoggedInUser != null)
        {
            Console.WriteLine($"👋 До свидания, {LoggedInUser.Username}!");
            LoggedInUser = null;
        }
        else
        {
            Console.WriteLine("⚠️ Вы не вошли в систему.");
        }
    }

    public bool IsAdmin() => LoggedInUser?.Role == UserRole.Admin;

    public void Deposit()
    {
        //Console.WriteLine($"Ваш текущий баланс: {Balance}");
        Console.WriteLine("Пополниет баланс:");

        decimal newBalance;
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out newBalance) && newBalance > 0)
                break;
            Console.WriteLine("Неверная сумма. Введите положительное число:");
        }

        using var conn = _db.GetConnection();
        decimal updateb = LoggedInUser.Balance + newBalance;
        using var sql = new NpgsqlCommand("UPDATE users SET balance =  @b WHERE id = @id", conn);
        sql.Parameters.AddWithValue("@b", updateb);
        sql.Parameters.AddWithValue("@id", LoggedInUser.Id);
        sql.ExecuteNonQuery();

        LoggedInUser.Balance += newBalance;

        Console.WriteLine($"Баланс пополнен. Новый баланс : {LoggedInUser.Balance} kzt");
    }


    public void ShowHistory()
    {
        using var conn = _db.GetConnection();
        using var sql = new NpgsqlCommand("SELECT * FROM orders WHERE customer_id = @id", conn);
        sql.Parameters.AddWithValue("@id", LoggedInUser.Id);

        using var reader = sql.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("У вас пока нет покупок.");
            return;
        }

        Console.WriteLine("История покупок:");

        int index = 1;
        while (reader.Read())
        {
            int brand = reader.GetInt32(0);
            int model = reader.GetInt32(1);
            DateTime date = reader.GetDateTime(3);

            Console.WriteLine($"{index++}. {brand} {model}-({date:dd.MM.yyyy})");
        }
    }
}