using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.GetSellerByEmail;

public class GetSellerByEmailHandler
{
    private readonly ISellerRepository _seller;
    public GetSellerByEmailHandler(ISellerRepository seller)
    {
        _seller = seller;
    }

    public async Task<Domain.Models.Seller> Handle(GetSellerByEmailQuery query)
    {
       return await _seller.GetSellerByEmail(query.email);
    }
}