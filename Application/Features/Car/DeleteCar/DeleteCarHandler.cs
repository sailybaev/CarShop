using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Car.DeleteCar;

public class DeleteCarHandler
{
    private readonly ICarRepository _repository;
    public DeleteCarHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteAsync(DeleteCarCommand command)
    {
        await _repository.DeleteAsync(await _repository.GetByIdAsync(command.carId));
    }
}