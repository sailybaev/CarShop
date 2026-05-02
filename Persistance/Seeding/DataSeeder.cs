using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Domain.ValueObjects;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Seeding;

public class DataSeeder
{
    private readonly CarDbContext _db;
    private readonly IPasswordHasher _hasher;

    public DataSeeder(CarDbContext db, IPasswordHasher hasher)
    {
        _db = db;
        _hasher = hasher;
    }

    public async Task SeedAsync()
    {
        // Re-seed if all listings have no images (stale seed data)
        if (await _db.Listings.AnyAsync())
        {
            var listings = await _db.Listings.ToListAsync();
            if (listings.Any(l => l.CarImages.Count > 0)) return;

            // Wipe stale data so we can re-seed with images
            _db.Listings.RemoveRange(listings);
            _db.Cars.RemoveRange(_db.Cars);
            _db.Sellers.RemoveRange(_db.Sellers);
            _db.User.RemoveRange(_db.User);
            await _db.SaveChangesAsync();
        }

        // Seller user
        var user = new User("dealer@carhub.com", _hasher.HashPassword("dealer123"), "PrivateSeller");
        await _db.User.AddAsync(user);
        await _db.SaveChangesAsync();

        var seller = new Seller(
            companyName: "CARHUB Demo",
            userId: user.Id,
            companyCity: "Almaty",
            companyAddress: "1 Showroom Ave",
            companyPhoneNumber: "+7 727 123 4567",
            companyEmail: "dealer@carhub.com"
        );
        await _db.Sellers.AddAsync(seller);
        await _db.SaveChangesAsync();

        // Cars + listings
        var seed = new[]
        {
            new
            {
                Car    = new Car("Tesla", "Model S Plaid",  2024, new VIN("1HGCM82633A123456"), new Money(89990m,  "USD")),
                City   = "Almaty",
                Desc   = "Ludicrous mode. 1020 hp. 0–60 in 1.99 s. Single owner, factory warranty.",
                Images = new List<string> { "https://images.unsplash.com/photo-1617788138017-80ad40651399?w=800&q=80" }
            },
            new
            {
                Car    = new Car("BMW",   "M4 Competition", 2024, new VIN("WBSDX9C51BC123457"), new Money(74900m,  "USD")),
                City   = "Astana",
                Desc   = "Competition package. Carbon fibre roof. BMW Individual paint. Every factory option.",
                Images = new List<string> { "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=800&q=80" }
            },
            new
            {
                Car    = new Car("Porsche", "911 Carrera",  2023, new VIN("WP0AA2A91JS123458"), new Money(115000m, "USD")),
                City   = "Almaty",
                Desc   = "PDK. Sport Chrono. PASM. Sport exhaust. Stunning in GT Silver Metallic.",
                Images = new List<string> { "https://images.unsplash.com/photo-1614162692292-7ac56d7f7f1e?w=800&q=80" }
            },
            new
            {
                Car    = new Car("Mercedes", "GLE 450",     2024, new VIN("4JGFB4JB8HA123459"), new Money(68500m,  "USD")),
                City   = "Shymkent",
                Desc   = "4MATIC hybrid. Burmester sound. Panoramic roof. AMG Line. 8k miles.",
                Images = new List<string> { "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=800&q=80" }
            },
            new
            {
                Car    = new Car("Audi",  "Q8 e-tron",      2024, new VIN("WA1LAAGE4JB123460"), new Money(72400m,  "USD")),
                City   = "Almaty",
                Desc   = "Electric quattro AWD. 300 mile range. Valcona leather. B&O 3D Premium Sound.",
                Images = new List<string> { "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800&q=80" }
            },
            new
            {
                Car    = new Car("Jaguar", "F-Type R",      2023, new VIN("SAJWA6BT4FMK23461"), new Money(98000m,  "USD")),
                City   = "Almaty",
                Desc   = "5.0L Supercharged V8, 575 hp. AWD. Carbon ceramic brakes. British Racing Green.",
                Images = new List<string> { "https://images.unsplash.com/photo-1544636331-e26879cd4d9b?w=800&q=80" }
            },
        };

        foreach (var item in seed)
        {
            await _db.Cars.AddAsync(item.Car);
            await _db.SaveChangesAsync();

            var listing = new Listing(seller.Id, null, item.Car, 0, item.City, item.Desc, item.Images);
            listing.Approve();
            await _db.Listings.AddAsync(listing);
            await _db.SaveChangesAsync();
        }
    }
}
