using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Infrastructure.Services;
using CarShopFinal.Persistance.Context;
using CarShopFinal.Persistance.Redis;
using CarShopFinal.Persistance.Repositories;
using CarShopFinal.Persistance.Seeding;
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
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IListingRepository, ListingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, JWTtokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IRedis, RedisDb>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<DataSeeder>();

        return services;
    }
}
