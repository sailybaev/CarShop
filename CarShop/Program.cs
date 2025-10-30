using System;
using CarShop.Enums;
using CarShop.Menus;
using CarShop.Services;

namespace CarShop;
//1
class Program
{
    static void Main(string[] args)
    {
        var authService = new AuthService();
        var carService = new CarService();

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