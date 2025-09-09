using EasyOrder.Application.DTOs;
using EasyOrder.Application.Interfaces;
using EasyOrder.Domain.Interfaces;

namespace EasyOrder.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Domain.Entities.Customer
            {
                CustomerId = Guid.NewGuid(),
                Name = createCustomerDto.Name,
                Email = createCustomerDto.Email,
                PhoneNumber = createCustomerDto.PhoneNumber,
                Address = createCustomerDto.Address
            };
            await _customerRepository.AddCustomerAsync(customer);
            return customer.CustomerId;
        }

        public Task<Domain.Entities.Customer?> GetCustomerByIdAsync(Guid customerId) => _customerRepository.GetCustomerByIdAsync(customerId);
        public Task<Domain.Entities.Customer?> GetCustomerByEmailAsync(string email) => _customerRepository.GetCustomerByEmailAsync(email);
        public Task UpdateCustomerAsync(Domain.Entities.Customer customer) => _customerRepository.UpdateCustomerAsync(customer);
        public Task DeleteCustomerAsync(Guid customerId) => _customerRepository.DeleteCustomerAsync(customerId);

    }
}
