using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Interfaces;

namespace EasyOrder.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order(dto.CustomerId);

        foreach (var item in dto.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _orderRepository.AddOrderAsync(order);
        return order.OrderId;
    }

    public Task<Order?> GetOrderByIdAsync(Guid orderId) => _orderRepository.GetOrderByIdAsync(orderId);

    public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId) => _orderRepository.GetOrdersByCustomerIdAsync(customerId);

    public Task UpdateOrderAsync(Order order) => _orderRepository.UpdateOrderAsync(order);

    public Task DeleteOrderAsync(Guid orderId) => _orderRepository.DeleteOrderAsync(orderId);

}
