using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Customer.GetCustomerById;

public class GetCustomerByIdHandle
{
    private readonly ICustomerRepository _customerRepository;
    
    public GetCustomerByIdHandle(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Domain.Models.Customer?> Handle(GetCustomerByIdQuery query)
    {
        return await _customerRepository.GetById(query.Id);
    }
}