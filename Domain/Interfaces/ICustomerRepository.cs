using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetById(Guid id);
    Task<Customer?> GetByEmail(string email);
    Task<Guid> AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}