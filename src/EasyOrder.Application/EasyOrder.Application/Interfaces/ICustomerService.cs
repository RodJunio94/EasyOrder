namespace EasyOrder.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(string name, string email);
        Task<Domain.Entities.Customer?> GetCustomerByIdAsync(Guid customerId);
        Task<Domain.Entities.Customer?> GetCustomerByEmailAsync(string email);
        Task UpdateCustomerAsync(Domain.Entities.Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}
