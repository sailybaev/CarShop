using CarShopFinal.Application.Features.Seller.GetSellerById;
using CarShopFinal.Application.Features.Seller.GetSellerByUserId;
using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.UpdateSeller;

public class UpdateSellerContactInfoHandler
{
    private readonly ISellerRepository _seller;
    public UpdateSellerContactInfoHandler(ISellerRepository seller) => _seller = seller;

    public async Task Handle(UpdateSellerContactInfoCommand query)
    {
        var seller = await _seller.GetSellerById(query.Id);
        if (seller == null)
            throw new Exception("Seller not found");
        seller.UpdateContactInfo(query.companyPhone, query.companyEmail);
        await _seller.UpdateSeller(seller);
    }
}