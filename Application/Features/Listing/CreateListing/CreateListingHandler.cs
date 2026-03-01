using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Listing.CreateListing;

public class CreateListingHandler
{
    private readonly IListingRepository _listingRepository;
    private readonly ICarRepository _carRepository;

    public CreateListingHandler(IListingRepository listingRepository, ICarRepository carRepository)
    {
        _listingRepository = listingRepository;
        _carRepository = carRepository;
    }

    public async Task<Guid> Handle(CreateListingCommand request)
    {
        //PRAVKA: Nujna mashina dlya togo chtoby zakinut' v listing
        var car = await _carRepository.GetByIdAsync(request.CarId)
            ?? throw new NotFoundException("Car", request.CarId);

        var listing = new Domain.Models.Listing(request.SellerId, null, car, 0, request.City,
            request.Description, request.CarImages);

        await _listingRepository.CreateListing(listing);
        return listing.Id;
    }
}
