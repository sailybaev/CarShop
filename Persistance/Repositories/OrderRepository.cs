using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Repositories;

public class OrderRepository:IOrderRepository
{
    private readonly CarDbContext _dbContext;
    public OrderRepository(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return  await _dbContext.Orders.FirstOrDefaultAsync(i=> i.Id == id);
    }

    public async Task<List<Order>?> GetByCustomerIdAsync(Guid customerId)
    {
        return await _dbContext.Orders.Where(i => i.CustomerId == customerId).ToListAsync<Order>();

    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}