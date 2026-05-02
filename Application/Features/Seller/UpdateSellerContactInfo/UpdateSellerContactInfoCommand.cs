namespace CarShopFinal.Application.Features.Seller.UpdateSeller;

public record UpdateSellerContactInfoCommand(Guid Id, string companyPhone, string companyEmail);