using CarShopFinal.Persistance.Repositories;

namespace CarShopFinal.Application.Features.Order.GetOrderById;

public class GetOrderByIdHandler
{
    private readonly OrderRepository _orderRepository;
    public GetOrderByIdHandler(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Domain.Models.Order?> Handle(GetOrderByIdQuery query)
    {
        return await _orderRepository.GetByIdAsync(query.id);
    }
}