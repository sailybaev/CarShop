using CarShop.Enums;
using CarShop.Menus;
using CarShop.Services;
using CarShop.Database;

namespace CarShop;

class Program
{
    // TODO: Seinur sdelai integration with SQL
    static void Main()
    {
        // Try to read connection string from environment, fall back to a reasonable default for local Postgres
        var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=carshop";

        var bbConnection = new BbConnection(connectionString);

        DbInitializer.Initialize(bbConnection);

        var authService = new AuthService(bbConnection);
        var carService = new CarService(bbConnection);

        while (true)
        {
            Console.WriteLine("\n📋 Главное меню:");
            Console.WriteLine("1 — Регистрация");
            Console.WriteLine("2 — Войти");
            Console.WriteLine("0 — Выход");

            Console.Write("➡️ Ваш выбор: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    authService.Register();
                    break;
                case "2":
                    authService.Login();
                    if (authService.LoggedInUser == null) break;

                    if (authService.LoggedInUser.Role == UserRole.Admin)
                        AdminMenu.Show(authService, carService);
                    else
                        ClientMenu.Show(authService, carService);
                    break;
                case "0":
                    Console.WriteLine("👋 До свидания!");
                    return;
                default:
                    Console.WriteLine("❌ Неверный выбор.");
                    break;
            }
        }
    }
}