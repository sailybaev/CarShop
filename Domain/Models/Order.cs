using CarShopFinal.Domain.Enums;
using CarShopFinal.Domain.ValueObjects;

namespace CarShopFinal.Domain.Models;

public class Order : AggregateRoot
{
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    
    private readonly List<OrderItem> _orderItems = new ();
    private readonly List<PaymentItem> _paymentItems = new ();
    
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public IReadOnlyCollection<PaymentItem> PaymentItems => _paymentItems.AsReadOnly();
    
    private Order() { }
    
    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        
        SetCreatedAt();
    }
    
    public void AddItem(Guid CarId, Money Price)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot add an order item to a paid/delivered/cancelled/refunded order");
        }
        var newItem = new OrderItem(CarId, Price);
        _orderItems.Add(newItem);
        SetUpdatedAt();
        
    }

    public Money GetTotal()
    {
        if (_orderItems.Count == 0)
        {
            return new Money(0, "USD");
        }

        var totalAmount = _orderItems.Sum(i=>i.price.Amount);

        return new Money(totalAmount , _orderItems[0].price.Currency);
    }
    

    public void MarkAsReserved()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException($"Order with status {Status} cannot be reserved.");
        }

        Status = OrderStatus.Reserved;
        SetUpdatedAt();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
        {
            throw new InvalidOperationException("Delivered orders can not be cancelled.");
        }

        Status = OrderStatus.Canceled;
        SetUpdatedAt();
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Reserved)
        {
            throw new InvalidOperationException($"Order with status {Status} cannot be marked as paid.");
        }

        Status = OrderStatus.Paid;
        SetUpdatedAt();
    }

    public void Deliver()
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Only paid orders can be delivered.");
        }

        Status = OrderStatus.Delivered;
        SetUpdatedAt();
    }

    public void Refund()
    {
        if (Status != OrderStatus.Paid && Status != OrderStatus.Delivered)
        {
            throw new InvalidOperationException("Only paid or delivered orders can be refunded.");
        }

        Status = OrderStatus.Refunded;
        SetUpdatedAt();
    }

    public void Payment(PaymentItem paymentItem)
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Only paid orders can be processed for payment.");
        }

        _paymentItems.Add(paymentItem);
    }
}