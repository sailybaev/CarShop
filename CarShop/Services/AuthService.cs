using CarShop.Enums;
using CarShop.Models;

namespace CarShop.Services;

public class AuthService
{
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

        _users.Add(new User(username, password, role, 100000)); // Начальный баланс 100000 для клиентов
        Console.WriteLine("✅ Пользователь зарегистрирован!");
    }

    public void Login()
    {
        Console.Write("Имя пользователя: ");
        string username = Console.ReadLine()!;
        Console.Write("Пароль: ");
        string password = Console.ReadLine()!;

        var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
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
}