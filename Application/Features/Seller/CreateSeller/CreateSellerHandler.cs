using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.CreateSeller;

public class CreateSellerHandler
{
    private readonly ISellerRepository _seller;
    
    public CreateSellerHandler(ISellerRepository seller)
    {
        _seller = seller;
    }

    public async Task<Guid> Handle(CreateSellerCommand request)
    {
        var seller = new Domain.Models.Seller(
            request.companyName,
            request.UserID,
            request.CompanyCity, 
            request.CompanyAddress,
            request.CompanyPhoneNumber, 
            request.CompanyEmail
            );
        await _seller.AddSeller(seller);
        return seller.Id;
    }
}