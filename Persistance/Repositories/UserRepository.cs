using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Repositories;

public class UserRepository:IUserRepository
{
    private readonly CarDbContext _context;
    
    public UserRepository(CarDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
       return await _context.User.FirstOrDefaultAsync(u => u.email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}