using System;
using CarShop.Database;
using CarShop.Enums;
using CarShop.Menus;
using CarShop.Models;
using CarShop.Services;

namespace CarShop;

class Program
{
    
    // TODO: Seinur sdelai integration with SQL
    static void Main(string[] args)
    {
        var authService = new AuthService();
        var carService = new CarService();
        var db = new BbConnection(
            "Host=ep-autumn-flower-a83va1s6-pooler.eastus2.azure.neon.tech;Username=neondb_owner;Password=npg_yAqlmK5gkoJ9;Database=neondb");
        var authRep = new AuthService(db);
        var carRep = new CarService(db);
        

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