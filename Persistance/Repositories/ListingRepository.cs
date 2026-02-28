using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;
using CarShopFinal.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShopFinal.Persistance.Repositories;

public class ListingRepository:IListingRepository
{
    private readonly CarDbContext _db;
    public ListingRepository(CarDbContext db)
    {
        _db = db;
    }
    
    public async Task CreateListing(Listing listing)
    {
        await _db.Listings.AddAsync(listing);
        await _db.SaveChangesAsync();
    }

    public async Task<Listing> GetListing(Guid id)
    {
        return await _db.Listings.FindAsync(id);
    }

    public async Task DeleteListing(Listing listing)
    {
        _db.Listings.Remove(listing);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateListing(Listing listing)
    { 
        _db.Listings.Update(listing);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Listing>> GetListingsByModerationStatus(string moderationStatus)
    {
        return await _db.Listings.Where(s => s.Status.ToString() == moderationStatus).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByListingStatus(string listingStatus)
    {
        return await _db.Listings.Where(s => s.ListingStatus.ToString() == listingStatus).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByCity(string city)
    {
        return await _db.Listings.Where(s => s.City.ToString() == city).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByBrand(string brand)
    {
        return await _db.Listings.Where(s => s.Car.Brand.ToString() == brand).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByModel(string model)
    {
        return await _db.Listings.Where(s => s.Car.Model.ToString() == model).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByYear(string year)
    {
        return await _db.Listings.Where(s => s.Car.Year.ToString() == year).ToListAsync();
    }

    public async Task<List<Listing>> GetListingsByPrice(decimal from, decimal to)
    {
        return await _db.Listings.Where(s => s.Car.Price.Amount >= from && s.Car.Price.Amount <= to).ToListAsync();
    }
}