using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}