namespace EasyOrder.Application.DTOs;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
