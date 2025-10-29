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

        public void ShowHistory()
        {
            using var conn = _db.GetConnection();
            using var sql = new NpgsqlCommand("SELECT * FROM orders WHERE customer_id = @id", conn);
            sql.Parameters.AddWithValue("@id", Id);

            using var reader = sql.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("У вас пока нет покупок.");
                return;
            }

            Console.WriteLine("История покупок:");

            int index = 1;
            while (reader.Read())
            {
                string brand = reader.GetString(0);
                string model = reader.GetString(1);
                DateTime date = reader.GetDateTime(3);

                Console.WriteLine($"{index++}. {brand} {model}-({date:dd.MM.yyyy})");
            }
        }


        public void Deposit()
        {
            Console.WriteLine($"Ваш текущий баланс: {Balance}");
            Console.WriteLine("Пополниет баланс:");

            decimal newBalance;
            while (true)
            {
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out newBalance) && newBalance > 0)
                    break;
                Console.WriteLine("Неверная сумма. Введите положительное число:");
            }
            
            using var conn = _db.GetConnection();
            using var sql = new NpgsqlCommand("UPDATE users SET balance = balance + @b WHERE customer_id = @id", conn);
            sql.Parameters.AddWithValue("@b", newBalance);
            sql.Parameters.AddWithValue("@id", Id);
            sql.ExecuteNonQuery();
            
            Balance += newBalance;

            Console.WriteLine($"Баланс пополнен. Новый баланс : {Balance} kzt");
        }
    }
    
}