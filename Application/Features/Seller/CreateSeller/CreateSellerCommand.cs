namespace CarShopFinal.Application.Features.Seller.CreateSeller;

public record CreateSellerCommand(
    string companyName,
    Guid UserID,
    string CompanyCity,
    string CompanyAddress,
    string CompanyPhoneNumber,
    string CompanyEmail
    );