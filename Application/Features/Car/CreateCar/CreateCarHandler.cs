using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.ValueObjects;

namespace CarShopFinal.Application.Features.Car.CreateCar;

public class CreateCarHandler
{
        private readonly ICarRepository _carRepository;

        public CreateCarHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Guid> Handle(CreateCarCommand command)
        {
            var car = new Domain.Models.Car(
                command.brand,
                command.model,
                command.year,
                command.vin,
                new Money(command.price, command.currency)
            );

            await _carRepository.AddAsync(car);

            return car.Id;
        }
    

}