using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Enums;

namespace EasyOrder.Domain.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalAmount { get; set; }

    public OrderCreatedEvent(Guid orderId, Guid customerId, DateTime orderDate, OrderStatus status, List<OrderItem> orderItems)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderDate = orderDate;
        Status = status;
        OrderItems = orderItems;
        TotalAmount = orderItems.Sum(item => item.UnitPrice * item.Quantity);
    }
}
