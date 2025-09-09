namespace EasyOrder.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Entities.Product product);
        Task<Entities.Product?> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
        Task UpdateProductAsync(Entities.Product product);
        Task DeleteProductAsync(Guid productId);
    }
}
