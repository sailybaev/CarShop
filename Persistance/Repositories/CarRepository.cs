using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Repositories;

public class CarRepository : ICarRepository
{
    
    private readonly CarDbContext _context;
    
    public CarRepository(CarDbContext context)
    {
        _context = context;
    }
    
    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _context.Cars.FindAsync(id);
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Car car)
    {
         _context.Cars.Remove(car);
    }

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Car>> GetFilteredCarAsync(string status)
    {
        return await _context.Cars.Where(c => c.Status.ToString() == status).ToListAsync<Car>();
    }

}
//copy and make redis, customer and order features and controllers, middleware? some new exceptions

