using CarShopFinal.Domain.Enums;
using CarShopFinal.Domain.ValueObjects;

namespace CarShopFinal.Domain.Models;

public class PaymentItem
{
    public Guid Id { get; private set; }
    public Money Price { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaidAt { get; private set; }
    //PRAVKA: Dobavil get set construct
    private PaymentItem() { }

    public PaymentItem(Money price)
    {
        Id = Guid.NewGuid();
        Price = price;
        Status = PaymentStatus.Pending;
        PaidAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        Status = PaymentStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
    }
}
