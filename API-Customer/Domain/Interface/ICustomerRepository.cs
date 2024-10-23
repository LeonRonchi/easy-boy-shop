using Domain.Model;

namespace Domain.Interface;

public interface ICustomerRepository
{
    Task<Customer> SaveAsync(Customer customer);
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(Guid id);
}
