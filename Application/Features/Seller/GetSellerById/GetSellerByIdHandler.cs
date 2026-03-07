using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Seller.GetSellerById;

public class GetSellerByIdHandler
{
    private readonly ISellerRepository _sellerRepository;

    public GetSellerByIdHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<Domain.Models.Seller> Handle(GetSellerByIdQuery query)
    {
        return await _sellerRepository.GetSellerById(query.id);
    }
}