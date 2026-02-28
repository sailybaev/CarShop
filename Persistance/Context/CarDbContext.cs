using Microsoft.EntityFrameworkCore;
using CarShopFinal.Domain.Models;
namespace CarShopFinal.Persistance.Context;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<User> User => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Listing> Listings => Set<Listing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarDbContext).Assembly);
    }
}