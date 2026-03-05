using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.NotFoundException;

namespace CarShopFinal.Application.Features.Order.CancelOrder;

public class CancelOrderHandler
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(CancelOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId)
            ?? throw new NotFoundException("Order", command.OrderId);

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
    }
}
