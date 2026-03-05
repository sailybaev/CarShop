using CarShopFinal.Application.Features.Order.CancelOrder;
using CarShopFinal.Application.Features.Order.CreateOrder;
using CarShopFinal.Application.Features.Order.GetOrderById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly CreateOrderHandle _createOrderHandle;
    private readonly GetOrderByIdHandler _getOrderByIdHandler;
    private readonly CancelOrderHandler _cancelOrderHandler;

    public OrderController(
        CreateOrderHandle createOrderHandle,
        GetOrderByIdHandler getOrderByIdHandler,
        CancelOrderHandler cancelOrderHandler)
    {
        _createOrderHandle = createOrderHandle;
        _getOrderByIdHandler = getOrderByIdHandler;
        _cancelOrderHandler = cancelOrderHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _createOrderHandle.CreateOrderAsync(command);
        return Ok(orderId);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _getOrderByIdHandler.Handle(new GetOrderByIdQuery(id));
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        await _cancelOrderHandler.Handle(new CancelOrderCommand(id));
        return NoContent();
    }
}
