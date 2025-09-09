using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        if (createOrderDto == null || createOrderDto.Items == null || !createOrderDto.Items.Any())
        {
            return BadRequest("Invalid order data.");
        }
        try
        {
            var orderId = await _orderService.CreateOrderAsync(createOrderDto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId }, new { OrderId = orderId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the order.");
        }
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order by ID.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the order.");
        }
    }

    [HttpGet("by-customer/{customerId:guid}")]
    public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId)
    {
        try
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders by customer ID.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the orders.");
        }
    }

    [HttpPut("{orderId:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderDto updateOrderDto)
    {
        if (updateOrderDto == null || updateOrderDto.Items == null || !updateOrderDto.Items.Any())
        {
            return BadRequest("Invalid order data.");
        }
        try
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.Items.Clear();
            foreach (var item in updateOrderDto.Items)
            {
                existingOrder.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            await _orderService.UpdateOrderAsync(existingOrder);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the order.");
        }
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        try
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                return NotFound();
            }
            await _orderService.DeleteOrderAsync(orderId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting order.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the order.");
        }
    }
}
