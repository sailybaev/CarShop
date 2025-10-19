using CarShop.Services;

namespace CarShop.Menus;

public static class AdminMenu
{
    public static void Show(AuthService auth, CarService carService)
    {
        while (auth.LoggedInUser != null)
        {
            Console.WriteLine("\n🔧 Админ меню:");
            Console.WriteLine("1 — Добавить машину");
            Console.WriteLine("2 — Удалить машину");
            Console.WriteLine("3 — Показать машины");
            Console.WriteLine("4 — Выйти из аккаунта");
            Console.WriteLine("0 — Назад");

            Console.Write("➡️ Выберите действие: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    carService.AddCar();
                    break;
                case "2":
                    carService.RemoveCar();
                    break;
                case "3":
                    carService.ShowCars();
                    break;
                case "4":
                    auth.Logout();
                    return;
                case "0":
                    return;
                default:
                    Console.WriteLine("❌ Неверный выбор.");
                    break;
            }
        }
    }
}