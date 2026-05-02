using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Listing.GetListingById;

public class GetListingByIdHandler
{
    private readonly IListingRepository _listingRepository;

    public GetListingByIdHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task<Domain.Models.Listing?> Handle(GetListingByIdQuery query)
    {
        return await _listingRepository.GetListing(query.Id);
    }
}
