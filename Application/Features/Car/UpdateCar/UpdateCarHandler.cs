using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.ValueObjects;

namespace CarShopFinal.Application.Features.Car.UpdateCar;

public class UpdateCarHandler
{
    private readonly ICarRepository _repository;
    public UpdateCarHandler(ICarRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UpdateCarCommand command)
    {
        var car = await _repository.GetByIdAsync(command.CarId);
        car.UpdateBrand(command.brand);
        car.UpdateModel(command.model);
        car.UpdateYear(command.year);
        car.UpdateVin(command.vin);
        car.UpdatePrice(new Money(command.price, command.currency));
        await _repository.UpdateAsync(car);
    }
}