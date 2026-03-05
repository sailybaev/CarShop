using CarShopFinal.Domain.Interfaces;

namespace CarShopFinal.Application.Features.Order.GetOrderById;

public class GetOrderByIdHandler
{
    // PRAVKA: Do etogo bylo OrderRepository vmesto IOrderRepository
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Domain.Models.Order?> Handle(GetOrderByIdQuery query)
    {
        return await _orderRepository.GetByIdAsync(query.id);
    }
}