using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Interfaces;

namespace EasyOrder.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();  

    public Task AddOrderAsync(Order order) => Task.Run(() => _orders.Add(order));

    public Task DeleteOrderAsync(Guid orderId) => Task.Run(() =>
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            _orders.Remove(order);
        }
    });

    public Task<Order?> GetOrderByIdAsync(Guid orderId) => Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == orderId));

    public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId) => Task.FromResult(_orders.Where(o => o.CustomerId == customerId).AsEnumerable());

    public Task UpdateOrderAsync(Order order) => Task.Run(() =>
    {
        var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if (existingOrder != null)
        {
            _orders.Remove(existingOrder);
            _orders.Add(order);
        }
    });
}
