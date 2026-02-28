using CarShopFinal.Application.Features.Car.GetFilteredCar;
using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface ICarRepository
{
    Task<Car?> GetByIdAsync(Guid id);
    Task<List<Car>> GetAllAsync();
    Task AddAsync(Car car);
        //Task UpdateAsync(Car car);
    Task DeleteAsync(Car car);
    Task UpdateAsync(Car car);
    Task<List<Car>> GetFilteredCarAsync(string status);
}