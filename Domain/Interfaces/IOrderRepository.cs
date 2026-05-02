using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>?> GetByCustomerIdAsync(Guid customerId);
    Task UpdateAsync(Order order);
}