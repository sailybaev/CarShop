using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Order.CreateOrder;

public class CreateOrderHandle
{
    private readonly IOrderRepository _orderRepository;
    public CreateOrderHandle(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> CreateOrderAsync(CreateOrderCommand command)
    {
        var order = new Domain.Models.Order(command.customerId);
        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}