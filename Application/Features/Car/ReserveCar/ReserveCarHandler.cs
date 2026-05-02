using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Car.ReserveCar;

public class ReserveCarHandler
{
    public readonly ICarRepository _carRepository;
    public ReserveCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task Handle(ReserveCarCommand command)
    {
        var car = await _carRepository.GetByIdAsync(command.Id) ?? throw new Exception("Car not found");
        car.Reserve();
        await _carRepository.UpdateAsync(car);
    }
}