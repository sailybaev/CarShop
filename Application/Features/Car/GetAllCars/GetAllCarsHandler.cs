using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;

namespace CarShopFinal.Application.Features.Car.GetAllCars;

public class GetAllCarsHandler
{
    private readonly ICarRepository _carRepository;
    
    public GetAllCarsHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    public async Task<List<Domain.Models.Car>> Handle(GetAllCarsQuery query)
    {
        return await _carRepository.GetAllAsync();
    }
}