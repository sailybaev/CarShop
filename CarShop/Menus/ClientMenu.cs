using CarShop.Services;

namespace CarShop.Menus;

public static class ClientMenu
{
    public static void Show(AuthService auth, CarService carService)
    {
        while (auth.LoggedInUser != null)
        {
            Console.WriteLine("\n👤 Меню клиента:");
            Console.WriteLine("1 — Показать машины");
            Console.WriteLine("2 — Купить машину");
            Console.WriteLine("3 — Выйти из аккаунта");
            Console.WriteLine("0 — Назад");

            Console.Write("➡️ Выберите действие: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    carService.ShowCars();
                    break;
                case "2":
                    carService.BuyCar(auth.LoggedInUser!);
                    break;
                case "3":
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