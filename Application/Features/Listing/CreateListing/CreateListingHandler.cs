using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Listing.CreateListing;

public class CreateListingHandler
{
    private readonly IListingRepository _listingRepository;

    public CreateListingHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task<Guid> Handle(CreateListingCommand request)
    {
        var listing = new Domain.Models.Listing(request.sellerId, request.status, request.car, request.view, request.city,
            request.description, request.carImages);
        
        await _listingRepository.CreateListing(listing);
        return listing.Id;
    }
}