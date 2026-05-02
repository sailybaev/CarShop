using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money() { }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        }

        Amount = amount;
        Currency = currency;
    }

    public override string ToString() => $"{Amount} {Currency}";
}