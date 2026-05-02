namespace CarShopFinal.Application.Features.Listing.UpdateListing;

public record UpdateListingCommand(Guid ListingId, string City, string Description);
