using CarShopFinal.Domain.Models;

namespace CarShopFinal.Domain.Interfaces;

public interface IListingRepository
{
    Task CreateListing(Listing listing);
    Task<Listing> GetListing(Guid id);
    Task DeleteListing(Listing listing);
    Task UpdateListing(Listing listing);
    Task<List<Listing>> GetListingsByModerationStatus(string moderationStatus);
    Task<List<Listing>> GetListingsByListingStatus(string listingStatus);
    Task<List<Listing>> GetListingsByCity(string city);
    Task<List<Listing>> GetListingsByBrand(string brand);
    Task<List<Listing>> GetListingsByModel(string model);
    Task<List<Listing>> GetListingsByYear(string year);
    Task<List<Listing>> GetListingsByPrice(decimal from, decimal to);
}