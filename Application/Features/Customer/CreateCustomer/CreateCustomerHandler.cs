using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Customer.CreateCustomer;

public class CreateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<Guid> Handle(CreateCustomerCommand request)
    {
        var customer = new Domain.Models.Customer(request.userID, request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        await _customerRepository.AddAsync(customer);
        return  customer.Id;
    }
}