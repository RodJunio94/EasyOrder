namespace EasyOrder.Application.DTOs;

public class UpdateOrderItemDto
{
    public Guid OrderItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid ProductId { get; set; }

}