using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Customer.UpdateCustomer;

public class UpdateCustomerHandler
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(UpdateCustomerCommand command)
    {
        var customer = await _customerRepository.GetById(command.CustomerId)
            ?? throw new NotFoundException("Customer", command.CustomerId);

        customer.UpdateContactInfo(command.Email, command.PhoneNumber);
        await _customerRepository.UpdateAsync(customer);
    }
}
