using CarShopFinal.Domain.Models;

namespace CarShopFinal.Application.Features.Car.CreateCar;

public record CreateCarCommand(string brand, string model, decimal price, string currency, int year, VIN vin);