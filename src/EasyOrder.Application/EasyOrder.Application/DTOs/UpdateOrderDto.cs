namespace EasyOrder.Application.DTOs;

public class UpdateOrderDto
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = null!;
    public List<UpdateOrderItemDto> Items { get; set; } = new();
}
