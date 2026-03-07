using CarShopFinal.Domain.Enums;
using CarShopFinal.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Domain.Models;

public class Car : AggregateRoot
{
    public Listing? Listing { get; private set; }
    public string Brand {get; private set; }
    public string Model {get; private set; }
    public int Year {get; private set; }
    
    public VIN Vin {get; private set; }
    public Money Price {get; private set; }
    
    public CarStatus Status {get; private set; }
    
    private Car() { }
    
    public Car(string brand, string model, int year, VIN vin, Money price)
    {
        ValidateBrand(brand);
        ValidateModel(model);
        ValidateYear(year);
        
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
        SetUpdatedAt();
    }
    
    public void MarkAsSold()
    {
        if(Status != CarStatus.Reserved) throw new InvalidOperationException("Only reserved cars can be marked as sold.");
        
        Status = CarStatus.Sold;
    }

    public void UpdatePrice(Money price)
    {
        Price = price;
        SetUpdatedAt();
    }
    

    public void UpdateVin(string vin)
    {
        Vin = new VIN(vin);
        SetUpdatedAt();
    }

    public void UpdateBrand(string brand)
    {
        ValidateBrand(brand);
        Brand = brand;
        SetUpdatedAt();
    }

    public void UpdateModel(string model)
    {
        ValidateModel(model);
        Model = model;
        SetUpdatedAt();
    }

    public void UpdateYear(int year)
    {
        ValidateYear(year);
        Year = year;
        SetUpdatedAt();
    }

    private void ValidateYear(int year)
    {
        var currentYear = DateTime.UtcNow.Year;
        
        if(year > currentYear)
            throw new ArgumentException("Year cannot be greater than current year.");

        if (year < 1886)
            throw new ArgumentException("Year cannot be earlier than 1886.");
    }

    private void ValidateBrand(string brand)
    {
        if(string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be empty.");
        if(brand.Length > 10)
            throw new ArgumentException("Brand cannot be longer than 10 characters.");
    }

    private void ValidateModel(string model)
    {
        if(string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty.");
        if(model.Length > 100)
            throw new ArgumentException("Model cannot be longer than 100 characters.");
    }
    
}