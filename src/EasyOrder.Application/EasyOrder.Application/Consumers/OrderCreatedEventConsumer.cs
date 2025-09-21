using EasyOrder.Domain.Events;
using EasyOrder.Domain.Interfaces;
using EasyOrder.Domain.Enums;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EasyOrder.Application.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventConsumer> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var orderCreatedEvent = context.Message;        
        
        _logger.LogInformation("OrderCreatedEvent recebido: OrderId={OrderId}, CustomerId={CustomerId}, TotalAmount={TotalAmount}", 
            orderCreatedEvent.OrderId, 
            orderCreatedEvent.CustomerId, 
            orderCreatedEvent.TotalAmount);

        
        await Task.Delay(10000);

        var order = await _orderRepository.GetOrderByIdAsync(orderCreatedEvent.OrderId);
        
        if (order != null)
        {
            
            order.Status = OrderStatus.paid;
                        
            await _orderRepository.UpdateOrderAsync(order);
            
            _logger.LogInformation("Status do pedido atualizado para {Status}: OrderId={OrderId}", 
                order.Status, orderCreatedEvent.OrderId);
        }
        else
        {
            _logger.LogWarning("Pedido não encontrado: OrderId={OrderId}", orderCreatedEvent.OrderId);
        }

        _logger.LogInformation("OrderCreatedEvent processado com sucesso: OrderId={OrderId}", orderCreatedEvent.OrderId);
        
        await Task.CompletedTask;
    }
}
