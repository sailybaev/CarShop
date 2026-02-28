using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Domain.Models;


public class VIN
{
    public string Value { get;}

    private VIN() { }
    
    public VIN(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("VIN cannot be null or empty.", nameof(value));
        }

        if (value.Length != 17)
        {
            throw new ArgumentException("VIN must be exactly 17 characters long.", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;
}