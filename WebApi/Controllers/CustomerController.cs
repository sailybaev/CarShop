using CarShopFinal.Application.Features.Customer.CreateCustomer;
using CarShopFinal.Application.Features.Customer.GetCustomerByEmail;
using CarShopFinal.Application.Features.Customer.GetCustomerById;
using CarShopFinal.Application.Features.Customer.UpdateCustomer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CreateCustomerHandler _createCustomerHandler;
    private readonly GetCustomerByIdHandle _getCustomerByIdHandle;
    private readonly GetCustomerByEmailHandle _getCustomerByEmailHandle;
    private readonly UpdateCustomerHandler _updateCustomerHandler;

    public CustomerController(
        CreateCustomerHandler createCustomerHandler,
        GetCustomerByIdHandle getCustomerByIdHandle,
        GetCustomerByEmailHandle getCustomerByEmailHandle,
        UpdateCustomerHandler updateCustomerHandler)
    {
        _createCustomerHandler = createCustomerHandler;
        _getCustomerByIdHandle = getCustomerByIdHandle;
        _getCustomerByEmailHandle = getCustomerByEmailHandle;
        _updateCustomerHandler = updateCustomerHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand customer)
    {
        var newCustomer = await _createCustomerHandler.Handle(customer);
        return Ok(newCustomer);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await _getCustomerByIdHandle.Handle(new GetCustomerByIdQuery(id));
        if (customer is null) return NotFound();
        return Ok(customer);
    }

    [Authorize]
    [HttpGet("by-email")]
    public async Task<IActionResult> GetCustomerByEmail([FromQuery] string email)
    {
        var customer = await _getCustomerByEmailHandle.GetByEmail(new GetCustomerByEmailQuery(email));
        if (customer is null) return NotFound();
        return Ok(customer);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerRequest request)
    {
        await _updateCustomerHandler.Handle(new UpdateCustomerCommand(id, request.Email, request.PhoneNumber));
        return NoContent();
    }
}

public record UpdateCustomerRequest(string Email, string PhoneNumber);

/* PRAVKI
   private readonly GetCustomerByIdHandle _getCustomerByIdHandle;
   private readonly GetCustomerByEmailHandle _getCustomerByEmailHandle;
   private readonly UpdateCustomerHandler _updateCustomerHandler;
*/
