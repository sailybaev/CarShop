using CarShopFinal.Domain.ValueObjects;

namespace CarShopFinal.Domain.Models;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid CarId { get; private set; }
    public Money price { get; private set; }

    private OrderItem() { }
    
    public OrderItem(Guid carId, Money price)
    {
        Id = Guid.NewGuid();
        CarId = carId;
        this.price = price;
    }
}