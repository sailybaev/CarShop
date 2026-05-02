using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Listing.RejectListing;

public class RejectListingHandler
{
    private readonly IListingRepository _listingRepository;

    public RejectListingHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task Handle(RejectListingCommand command)
    {
        var listing = await _listingRepository.GetListing(command.ListingId)
            ?? throw new NotFoundException("Listing", command.ListingId);

        listing.Reject();
        await _listingRepository.UpdateListing(listing);
    }
}
