using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Car.GetFilteredCar;

public class GetFilteredCarHandler
{
    private readonly ICarRepository _repository;
    public GetFilteredCarHandler(ICarRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Domain.Models.Car>> Handle(GetFilteredCarQuery query)
    {
        return await _repository.GetFilteredCarAsync(query.status);
    }
}