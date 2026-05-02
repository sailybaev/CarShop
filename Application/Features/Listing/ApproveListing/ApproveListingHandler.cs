using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Listing.ApproveListing;

public class ApproveListingHandler
{
    private readonly IListingRepository _listingRepository;

    public ApproveListingHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task Handle(ApproveListingCommand command)
    {
        var listing = await _listingRepository.GetListing(command.ListingId)
            ?? throw new NotFoundException("Listing", command.ListingId);

        listing.Approve();
        await _listingRepository.UpdateListing(listing);
    }
}
