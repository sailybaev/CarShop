using CarShop.Enums;
using CarShop.Models;
using CarShop.Database;
using Npgsql;

namespace CarShop.Services;

public class AuthService
{
    private readonly BbConnection _bbConnection;
    public User? LoggedInUser { get; private set; }

    public AuthService(BbConnection bbConnection)
    {
        _bbConnection = bbConnection;
    }

    private static User MapUser(NpgsqlDataReader reader)
    {
        var username = reader.GetString(reader.GetOrdinal("username"));
        var password = reader.GetString(reader.GetOrdinal("password"));
        var roleStr = reader.GetString(reader.GetOrdinal("role"));
        var balance = reader.GetDecimal(reader.GetOrdinal("balance"));

        var role = Enum.TryParse<UserRole>(roleStr, out var r) ? r : UserRole.Client;
        return new User(username, password, role, balance);
    }

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

        // Default starting balance for clients, 0 for admins
        decimal startingBalance = role == UserRole.Client ? 100000m : 0m;

        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand(@"INSERT INTO users (username, password, role, balance) VALUES (@u, @p, @r, @b)", conn);
        cmd.Parameters.AddWithValue("@u", username);
        cmd.Parameters.AddWithValue("@p", password);
        cmd.Parameters.AddWithValue("@r", role.ToString());
        cmd.Parameters.AddWithValue("@b", startingBalance);

        try
        {
            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ Пользователь зарегистрирован!");
        }
        catch (PostgresException ex) when (ex.SqlState == "23505") // unique_violation
        {
            Console.WriteLine("❌ Пользователь с таким именем уже существует.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при регистрации: {ex.Message}");
        }
    }

    public void Login()
    {
        Console.Write("Имя пользователя: ");
        string username = Console.ReadLine()!;
        Console.Write("Пароль: ");
        string password = Console.ReadLine()!;

        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand("SELECT username, password, role, balance FROM users WHERE username = @u LIMIT 1", conn);
        cmd.Parameters.AddWithValue("@u", username);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read())
        {
            Console.WriteLine("❌ Неверные данные для входа!");
            return;
        }

        var user = MapUser(reader);

        if (user.Password != password)
        {
            Console.WriteLine("❌ Неверные данные для входа!");
            return;
        }

        LoggedInUser = user;
        Console.WriteLine($"👋 Добро пожаловать, {user.Username} ({user.Role})!");
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
        if (LoggedInUser == null)
        {
            Console.WriteLine("⚠️ Вы не вошли в систему.");
            return;
        }

        Console.WriteLine("Пополните баланс:");
        decimal amount;
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out amount) && amount > 0)
                break;
            Console.WriteLine("Неверная сумма. Введите положительное число:");
        }

        using var conn = _bbConnection.GetConnection();
        using var cmd = new NpgsqlCommand("UPDATE users SET balance = balance + @amt WHERE username = @u RETURNING balance", conn);
        cmd.Parameters.AddWithValue("@amt", amount);
        cmd.Parameters.AddWithValue("@u", LoggedInUser.Username);

        try
        {
            var result = cmd.ExecuteScalar();
            if (result != null && decimal.TryParse(result.ToString(), out var newBalance))
            {
                LoggedInUser.Balance = newBalance;
                Console.WriteLine($"✅ Баланс пополнен. Новый баланс: {LoggedInUser.Balance} KZT");
            }
            else
            {
                Console.WriteLine("❌ Не удалось обновить баланс пользователя.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка при пополнении баланса: {ex.Message}");
        }
    }
}