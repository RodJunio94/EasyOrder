using EasyOrder.Application.DTOs;

namespace EasyOrder.Application.Interfaces;

public interface IProductService
{
    Task<Guid> CreateProductAsync(CreateProductDto productDto);
    Task<Domain.Entities.Product?> GetProductByIdAsync(Guid productId);
    Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync();
    Task UpdateProductAsync(Domain.Entities.Product product);
    Task DeleteProductAsync(Guid productId);
}
