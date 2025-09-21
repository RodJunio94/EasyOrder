using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Events;
using EasyOrder.Domain.Interfaces;
using MassTransit;

namespace EasyOrder.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBus _bus;
    public OrderService(IOrderRepository orderRepository, IBus bus)
    {
        _orderRepository = orderRepository;
        _bus = bus;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order(dto.CustomerId);

        foreach (var item in dto.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _orderRepository.AddOrderAsync(order);

        var eventRequest = new OrderCreatedEvent(order.OrderId, order.CustomerId, order.OrderDate, order.Status, order.OrderItems);

        await _bus.Publish(eventRequest);

        return new OrderResponseDto
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            Items = order.OrderItems.Select(item => new OrderItemResponseDto
            {
                OrderItemId = item.OrderItemId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Subtotal = item.UnitPrice * item.Quantity
            }).ToList()
        };
    }

    public Task<Order?> GetOrderByIdAsync(Guid orderId) => _orderRepository.GetOrderByIdAsync(orderId);

    public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId) => _orderRepository.GetOrdersByCustomerIdAsync(customerId);

    public Task UpdateOrderAsync(Order order) => _orderRepository.UpdateOrderAsync(order);

    public Task DeleteOrderAsync(Guid orderId) => _orderRepository.DeleteOrderAsync(orderId);

}
