using EasyOrder.Domain.Enums;

namespace EasyOrder.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();

    protected Order() { }

    public decimal TotalAmount => OrderItems.Sum(item => item.UnitPrice * item.Quantity);

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        var item = new OrderItem(productId, quantity, unitPrice);
        OrderItems.Add(item);
    }
    public Order(Guid customerId)
    {
        OrderId = Guid.NewGuid();
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }
    public List<Order> Items { get; set; } = new();
}
