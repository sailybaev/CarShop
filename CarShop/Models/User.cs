using CarShop.Enums;
using Npgsql;
using CarShop.Database;

namespace CarShop.Models
{
    public class User
    {
        private readonly BbConnection _db;

        public User(BbConnection db)
        {
            _db = db;
        }
        public int Id { get; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public UserRole Role { get; set; }
        
        public List<Car> BoughtCars { get; set; }

        public User(int id, string username, string password, UserRole role, decimal balance = 0, List <Car>? BoughtCars = null)
        {
            Id = id;
            this.BoughtCars = BoughtCars ?? new List<Car>();
            Username = username;
            Password = password;
            Role = role;
            Balance = balance;
        }
        
    }
    
    
}