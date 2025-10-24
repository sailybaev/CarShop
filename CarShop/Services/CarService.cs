using CarShop.Models;

namespace CarShop.Services;

public class CarService
{
    private readonly List<Car> _cars = new();
    private int _nextId = 1;

    public void ShowCars() // Pokazat' spisok mashin
    {
        if (_cars.Count == 0)
        {
            Console.WriteLine("Нет доступных машин.");
            return;
        }

        Console.WriteLine("\n🚗 Список машин:");
        foreach (var car in _cars)
            Console.WriteLine(car);
    }

    public void AddCar()
    {
        Console.Write("Введите название машины: ");
        string name = Console.ReadLine()!;
        Console.Write("Введите цену машины: ");

        decimal price;
        while (true)
        {
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out price) && price >= 0)
                break;
            Console.Write("Неверная цена. Введите положительное число: ");
        }

        var car = new Car(_nextId++, name, price);
        _cars.Add(car);
        Console.WriteLine("✅ Машина добавлена!");
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

        var car = _cars.FirstOrDefault(c => c.Id == id);
        if (car != null)
        {
            _cars.Remove(car);
            Console.WriteLine("✅ Машина удалена.");
        }
        else
        {
            Console.WriteLine("❌ Машина не найдена.");
        }
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

        var car = _cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            Console.WriteLine("❌ Машина не найдена.");
            return;
        }

        if (user.Balance < car.Price)
        {
            Console.WriteLine("❌ Недостаточно средств!");
            return;
        }

        user.Balance -= car.Price;
        user.BoughtCars.Add(car);
        _cars.Remove(car);
        Console.WriteLine($"✅ Поздравляем с покупкой {car.Name}! Ваш баланс: {user.Balance} KZT");


    }
}