namespace EasyOrder.Domain.Interfaces;

public interface ICustomerRepository
{
    Task AddCustomerAsync(Entities.Customer customer);
    Task<Entities.Customer?> GetCustomerByIdAsync(Guid customerId);
    Task<Entities.Customer?> GetCustomerByEmailAsync(string email);
    Task UpdateCustomerAsync(Entities.Customer customer);
    Task DeleteCustomerAsync(Guid customerId);
}
