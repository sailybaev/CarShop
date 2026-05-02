using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Listing.UpdateListing;

public class UpdateListingHandler
{
    private readonly IListingRepository _listingRepository;

    public UpdateListingHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task Handle(UpdateListingCommand command)
    {
        var listing = await _listingRepository.GetListing(command.ListingId)
            ?? throw new NotFoundException("Listing", command.ListingId);

        listing.Update(command.City, command.Description);
        await _listingRepository.UpdateListing(listing);
    }
}
