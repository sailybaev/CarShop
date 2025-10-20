using CarShop.Enums;

namespace CarShop.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public UserRole Role { get; set; }
        
        public List<Car> BoughtCars { get; set; }

        public User(string username, string password, UserRole role, decimal balance = 0, List <Car>? BoughtCars = null)
        {
            Username = username;
            Password = password;
            Role = role;
            Balance = balance;
        }

        void ShowHistory()
        {
            Console.WriteLine("Istoriya Vasich pokupok:");
            foreach (var car in BoughtCars)
            {
                Console.WriteLine(car);
            }
        }

        public void Deposit()
        {
            Console.WriteLine("Popolnite balans:");
            decimal newBalance = decimal.Parse(Console.ReadLine());
            Balance += newBalance;
        }
    }
    
}