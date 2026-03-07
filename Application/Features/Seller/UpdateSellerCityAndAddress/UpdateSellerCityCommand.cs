namespace CarShopFinal.Application.Features.Seller.UpdateSellerCity;

public record UpdateSellerCityCommand(Guid id, string CompanyCity,  string CompanyAddress);