namespace CarShopFinal.Application.Features.Car.UpdateCar;

public record UpdateCarCommand(Guid CarId, string brand, string model, int year, string vin, decimal price, string currency);