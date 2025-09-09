using EasyOrder.Domain.Entities;

namespace EasyOrder.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Guid orderId);
}
