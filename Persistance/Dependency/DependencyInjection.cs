using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Persistance.Context;
using CarShopFinal.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<ITokenService,JWTtokenService>();
        services.AddScoped<IPasswordHasher,PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}