using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface ISellerRepository
{
    Task<Seller?> GetSellerById(Guid id);
    Task<Seller?> GetSellerByEmail(string email);
    Task<Seller?> GetSellerByUserId(Guid userId);
    Task<Guid> AddSeller(Seller seller);
    Task UpdateSeller(Seller seller);
    
}