using CarShopFinal.Application.Features.Seller.GetSellerById;
using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.GetSellerByUserId;

public class GetSellerByUserIDHandler
{
    private readonly ISellerRepository _sellerRepository;
    public GetSellerByUserIDHandler(ISellerRepository sellerRepository) => _sellerRepository = sellerRepository;

    public async Task<Domain.Models.Seller> Handle(GetSellerByUserIDQuery query)
    {
        return await _sellerRepository.GetSellerByUserId(query.UserId);
    }
}