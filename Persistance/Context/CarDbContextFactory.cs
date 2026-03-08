using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarShopFinal.Persistance.Context;

public class CarDbContextFactory:IDesignTimeDbContextFactory<CarDbContext>
{
    public CarDbContext CreateDbContext(string[] args)
    {
        var p = new DbContextOptionsBuilder<CarDbContext>();
        p.UseNpgsql("Host=localhost;Port=5432;Database=carshop;Username=postgres;Password=password");
        return new CarDbContext(p.Options);
    }
}