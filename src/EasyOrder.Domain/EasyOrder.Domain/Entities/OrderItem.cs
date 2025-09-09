namespace EasyOrder.Domain.Entities;

public class OrderItem
{
    public Guid OrderItemId { get; set; } 
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    protected OrderItem() { }

    public OrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        OrderItemId = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
