using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using EasyOrder.Domain.Interfaces;
using MassTransit;

namespace EasyOrder.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Guid> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Domain.Entities.Product
        {
            ProductId = Guid.NewGuid(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity,
            CreatedAt = DateTime.UtcNow
        };

        await _productRepository.AddProductAsync(product);

        return product.ProductId;
    }
    public Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync() => _productRepository.GetAllProductsAsync();
    public Task<Domain.Entities.Product?> GetProductByIdAsync(Guid productId) => _productRepository.GetProductByIdAsync(productId);
    public Task UpdateProductAsync(Domain.Entities.Product product) => _productRepository.UpdateProductAsync(product);
    public Task DeleteProductAsync(Guid productId) => _productRepository.DeleteProductAsync(productId);
}
