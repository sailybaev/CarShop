using CarShopFinal.Domain.Enums;

namespace CarShopFinal.Application.Features.Listing.CreateListing;

public record CreateListingCommand(Guid sellerId, ModerationStatus? status, Domain.Models.Car car, int view, string city,string description, List<Guid> carImages);
