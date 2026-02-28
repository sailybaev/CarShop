using CarShopFinal.Domain.Enums;
using CarShopFinal.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Domain.Models;

public class Car : AggregateRoot
{
    public Listing Listing { get; private set; }
    public string Brand {get; private set; }
    public string Model {get; private set; }
    public int Year {get; private set; }
    
    public VIN Vin {get; private set; }
    public Money Price {get; private set; }
    
    public CarStatus Status {get; private set; }
    
    private Car() { }
    
    public Car(string brand, string model, int year, VIN vin, Money price)
    {
        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        Year = year;
        Vin = vin;
        Price = price;
        Status = CarStatus.InStock;
        SetCreatedAt();
    }

    public void Reserve()
    {
        if(Status != CarStatus.InStock) throw new InvalidOperationException("Only cars in stock can be reserved.");
        
        Status = CarStatus.Reserved;
    }
    
    public void MarkAsSold()
    {
        if(Status != CarStatus.Reserved) throw new InvalidOperationException("Only reserved cars can be marked as sold.");
        
        Status = CarStatus.Sold;
    }

    public void UpdatePrice(Money price)
    {
        Price = price;
    }

    public void UpdateVin(string vin)
    {
        Vin = new VIN(vin);
    }

    public void UpdateBrand(string brand)
    {
        Brand = brand;
    }

    public void UpdateModel(string model)
    {
        Model = model;
    }

    public void UpdateYear(int year)
    {
        Year = year;
    }
}