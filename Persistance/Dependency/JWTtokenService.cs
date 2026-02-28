using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarShopFinal.Persistance.Dependency;

public class JWTtokenService:ITokenService
{
    private readonly IConfiguration _configuration;

    public JWTtokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, user.role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
        
        var hash = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:issuer"],
            audience: _configuration["jwt:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: hash
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}