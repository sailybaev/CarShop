using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.UpdateSellerCity;

public class UpdateSellerCityHandler
{
    private readonly ISellerRepository _sellerRepository;
    public UpdateSellerCityHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task Handle(UpdateSellerCityCommand request)
    {
        var seller = await _sellerRepository.GetSellerById(request.id);
        if (seller == null)
            throw new Exception("Seller not found");
        seller.UpdateCityAndAddress(request.CompanyCity, request.CompanyAddress);
        await _sellerRepository.UpdateSeller(seller);
    }
}