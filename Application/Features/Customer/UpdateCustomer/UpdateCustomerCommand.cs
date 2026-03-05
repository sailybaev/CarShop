namespace CarShopFinal.Application.Features.Customer.UpdateCustomer;

public record UpdateCustomerCommand(Guid CustomerId, string Email, string PhoneNumber);
