namespace CarShop.Models
{
    public class Car
    {
        public int Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Car(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Id}: {Name} — {Price} KZT";
        }
    }
}