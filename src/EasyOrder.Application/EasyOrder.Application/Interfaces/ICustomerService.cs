using EasyOrder.Application.DTOs;

namespace EasyOrder.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<Domain.Entities.Customer?> GetCustomerByIdAsync(Guid customerId);
        Task<Domain.Entities.Customer?> GetCustomerByEmailAsync(string email);
        Task UpdateCustomerAsync(Domain.Entities.Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}
