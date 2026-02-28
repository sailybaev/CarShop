using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
}