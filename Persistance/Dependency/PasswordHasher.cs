using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Persistance.Dependency;

public class PasswordHasher:IPasswordHasher
{
    public string HashPassword(string password)
    {
        return  BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return  BCrypt.Net.BCrypt.Verify(hashedPassword, providedPassword);
    }
}