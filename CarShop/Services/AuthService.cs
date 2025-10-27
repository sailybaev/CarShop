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
        using var sql2 = new NpgsqlCommand("INSERT INTO users(id,name,password,role) VALUES(@id,@name,@password,@role)", conn);
        sql2.Parameters.AddWithValue("@name", username);
        sql2.Parameters.AddWithValue("@password", password);
        sql2.Parameters.AddWithValue("@role", roleChoice);
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
            using var sql =  new NpgsqlCommand("SELECT id, username, password, role, balance FROM users WHERE username = @u AND password = @p LIMIT 1", conn);
            sql.Parameters.AddWithValue("@u", username);
            sql.Parameters.AddWithValue("@p", password);

            using var reader = sql.ExecuteReader();
            if (!reader.Read())
            {
                Console.WriteLine("Неверные данные для входа!");
                return;
            }

            int id = reader.GetInt32(0);
            string usern = reader.GetString(1);
            int role = reader.GetInt32(3);
            decimal balance = reader.GetDecimal(4);

            LoggedInUser = new User(usern, password, (UserRole)role, balance);
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
}