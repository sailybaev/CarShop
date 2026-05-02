namespace CarShopFinal.Application.Features.Customer.CreateCustomer;

public record CreateCustomerCommand(
    Guid userID,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);