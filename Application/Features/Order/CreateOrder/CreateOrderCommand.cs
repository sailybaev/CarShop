using System.Windows.Input;

namespace CarShopFinal.Application.Features.Order.CreateOrder;

public record CreateOrderCommand(Guid customerId);
