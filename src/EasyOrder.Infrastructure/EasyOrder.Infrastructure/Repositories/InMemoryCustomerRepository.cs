using EasyOrder.Domain.Entities;
using EasyOrder.Domain.Interfaces;

namespace EasyOrder.Infrastructure.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new();
    public Task AddCustomerAsync(Customer customer)
    {
        _customers.Add(customer);
        return Task.CompletedTask;
    }

    public Task DeleteCustomerAsync(Guid customerId)
    {
        var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer != null)
            _customers.Remove(customer);
        return Task.CompletedTask;
    }

    public Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var customer = _customers.FirstOrDefault(c => c.Email == email);
        return Task.FromResult(customer);
    }

    public Task<Customer?> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
        return Task.FromResult(customer);
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        var existing = _customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
        if (existing != null)
        {
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
        }
        return Task.CompletedTask;
    }

}
