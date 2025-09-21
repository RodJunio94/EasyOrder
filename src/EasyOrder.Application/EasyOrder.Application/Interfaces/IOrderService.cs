using EasyOrder.Application.DTOs;
using EasyOrder.Domain.Entities;

namespace EasyOrder.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
    Task<Domain.Entities.Order?> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<Domain.Entities.Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Guid orderId);
}
