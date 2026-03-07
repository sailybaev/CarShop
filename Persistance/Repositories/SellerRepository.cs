using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;

namespace CarShopFinal.Persistance.Repositories;

public class SellerRepository:ISellerRepository
{
    private readonly CarDbContext _context;

    public SellerRepository(CarDbContext context)
    {
        _context = context;
    }
    
    public async Task<Seller?> GetSellerById(Guid id)
    {
       return await _context.Sellers.FindAsync(id);
    }

    public async Task<Seller?> GetSellerByEmail(string email)
    {
        return await _context.Sellers.FindAsync(email);
    }

    public async Task<Seller?> GetSellerByUserId(Guid userId)
    {
        return await _context.Sellers.FindAsync(userId);
    }

    public async Task<Guid> AddSeller(Seller seller)
    {
        _context.Sellers.Add(seller);
        await _context.SaveChangesAsync();
        return seller.Id;
    }

    public Task UpdateSeller(Seller seller)
    {
        _context.Sellers.Update(seller);
        return _context.SaveChangesAsync();
    }
}