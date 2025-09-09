using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Interfaces;

namespace EasyOrder.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _product = new();

    public Task AddProductAsync(Product product)
    {
        _product.Add(product);
        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(Product product) {
                _product.Remove(product);
        return Task.CompletedTask;
    }

    public Task<Product?> GetProductByIdAsync(Guid productId)
    {
        var product = _product.FirstOrDefault(p => p.ProductId == productId);
        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return Task.FromResult(_product.AsEnumerable());
    }

    public Task UpdateProductAsync(Product product)
    {
        var existing = _product.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
        }
        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(Guid productId) => Task.Run(() =>
    {
        var product = _product.FirstOrDefault(p => p.ProductId == productId);
        if (product != null)
        {
            _product.Remove(product);
        }
    });

}
