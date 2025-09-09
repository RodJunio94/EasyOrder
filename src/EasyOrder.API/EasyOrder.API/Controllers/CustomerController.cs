using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;
    public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        if (createCustomerDto == null || string.IsNullOrWhiteSpace(createCustomerDto.Name) || string.IsNullOrWhiteSpace(createCustomerDto.Email))
        {
            return BadRequest("Invalid customer data.");
        }
        try
        {
            var customerId = await _customerService.CreateCustomerAsync(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId }, new { CustomerId = customerId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the customer.");
        }
    }
    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid customerId)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving customer by ID.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the customer.");
        }
    }
    [HttpGet("by-email")]
    public async Task<IActionResult> GetCustomerByEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest("Email is required.");
        }
        try
        {
            var customer = await _customerService.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving customer by email.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the customer.");
        }
    }
    [HttpPut("{customerId:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerDto updateCustomerDto)
    {
        if (updateCustomerDto == null || string.IsNullOrWhiteSpace(updateCustomerDto.Name) || string.IsNullOrWhiteSpace(updateCustomerDto.Email))
        {
            return BadRequest("Invalid customer data.");
        }
        try
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = updateCustomerDto.Name;
            existingCustomer.Email = updateCustomerDto.Email;
            existingCustomer.PhoneNumber = updateCustomerDto.PhoneNumber;
            existingCustomer.Address = updateCustomerDto.Address;
            await _customerService.UpdateCustomerAsync(existingCustomer);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");
        }
    }
    [HttpDelete("{customerId:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid customerId)
    {
        try
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            await _customerService.DeleteCustomerAsync(customerId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the customer.");
        }
    }

}
