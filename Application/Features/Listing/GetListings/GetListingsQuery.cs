namespace CarShopFinal.Application.Features.Listing.GetListings;

public record GetListingsQuery(
    string? City = null,
    string? Brand = null,
    string? Model = null,
    string? Year = null,
    decimal? PriceFrom = null,
    decimal? PriceTo = null,
    string? ListingStatus = null,
    string? ModerationStatus = null
);
