using CarShop.Enums;

namespace CarShop.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public UserRole Role { get; set; }

        public User(string username, string password, UserRole role, decimal balance = 0)
        {
            Username = username;
            Password = password;
            Role = role;
            Balance = balance;
        }
    }
}