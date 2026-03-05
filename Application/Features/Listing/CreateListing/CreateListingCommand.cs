using CarShopFinal.Domain.Enums;

namespace CarShopFinal.Application.Features.Listing.CreateListing;

public record CreateListingCommand(Guid SellerId, Guid CarId, string City, string Description, List<string> CarImages);
