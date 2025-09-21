using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EasyOrder.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name) || productDto.Price <= 0 || productDto.StockQuantity < 0)
        {
            return BadRequest("Invalid product data.");
        }
        try
        {
            var productId = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { productId }, new { ProductId = productId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the product.");
        }
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product by ID.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the product.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
        }
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] CreateProductDto productDto)
    {
        if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name) || productDto.Price <= 0 || productDto.StockQuantity < 0)
        {
            return BadRequest("Invalid product data.");
        }
        try
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.StockQuantity = productDto.StockQuantity;
            await _productService.UpdateProductAsync(existingProduct);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the product.");
        }
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        try
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product.");
        }
    }
}
