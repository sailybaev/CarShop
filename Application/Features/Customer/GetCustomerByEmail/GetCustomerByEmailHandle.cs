using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Customer.GetCustomerByEmail;

public class GetCustomerByEmailHandle
{
    private readonly ICustomerRepository _customerRepository;
    public GetCustomerByEmailHandle(ICustomerRepository customerRepository) => _customerRepository = customerRepository;

    public async Task<Domain.Models.Customer?> GetByEmail(GetCustomerByEmailQuery query)
    {
        return await _customerRepository.GetByEmail(query.email);
    }
}