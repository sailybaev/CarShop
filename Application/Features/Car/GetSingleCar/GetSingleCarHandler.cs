using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Car.GetSingleCar;

public class GetSingleCarHandler
{
    private readonly ICarRepository _repository;
    public GetSingleCarHandler(ICarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Models.Car> Handle(GetSingleCarQuery command)
    {
        return await  _repository.GetByIdAsync(command.CarId);
    }
}