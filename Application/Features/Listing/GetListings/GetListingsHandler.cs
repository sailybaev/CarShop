using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Listing.GetListings;

public class GetListingsHandler
{
    private readonly IListingRepository _listingRepository;

    public GetListingsHandler(IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    public async Task<List<Domain.Models.Listing>> Handle(GetListingsQuery query)
    {
        if (query.City != null)
            return await _listingRepository.GetListingsByCity(query.City);

        if (query.Brand != null)
            return await _listingRepository.GetListingsByBrand(query.Brand);

        if (query.Model != null)
            return await _listingRepository.GetListingsByModel(query.Model);

        if (query.Year != null)
            return await _listingRepository.GetListingsByYear(query.Year);

        if (query.PriceFrom != null || query.PriceTo != null)
            return await _listingRepository.GetListingsByPrice(query.PriceFrom ?? 0, query.PriceTo ?? decimal.MaxValue);

        if (query.ModerationStatus != null)
            return await _listingRepository.GetListingsByModerationStatus(query.ModerationStatus);

        if (query.ListingStatus != null)
            return await _listingRepository.GetListingsByListingStatus(query.ListingStatus);

        return await _listingRepository.GetListingsByListingStatus("Published");
    }
}
