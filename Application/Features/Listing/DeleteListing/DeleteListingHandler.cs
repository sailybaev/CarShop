using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Listing.DeleteListing;

public class DeleteListingHandler
{
    private readonly IListingRepository _listingRepository;

    public DeleteListingHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task Handle(DeleteListingCommand command)
    {
        var listing = await _listingRepository.GetListing(command.ListingId)
            ?? throw new NotFoundException("Listing", command.ListingId);

        await _listingRepository.DeleteListing(listing);
    }
}
