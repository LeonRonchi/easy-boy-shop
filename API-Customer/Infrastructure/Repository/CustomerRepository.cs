using Domain.Exception;
using Domain.Interface;
using Domain.Model;
using Infrastructure.Interface;

namespace Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ICustomerScyllaRepository _customerRepository;
    private readonly ICustomerAdapter _customerAdapter;

    public CustomerRepository(ICustomerScyllaRepository customerRepository, ICustomerAdapter customerAdapter)
    {
        _customerRepository = customerRepository;
        _customerAdapter = customerAdapter;
    }

    public async Task<Customer> SaveAsync(Customer customer)
    {
        var entity = _customerAdapter.ToCustomerEntity(customer);
        var storedEntity = await _customerRepository.SaveAsync(entity);
        var storedCustomer = _customerAdapter.FromCustomerEntity(storedEntity);

        return storedCustomer;
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        var storedEntity = await _customerRepository.GetByIdAsync(id);

        if (storedEntity == null)
            return null;               

        return _customerAdapter.FromCustomerEntity(storedEntity);
    }

    public async Task<IEnumerable<Customer>?> GetCustomersAsync()
    {
        var storedEntity = await _customerRepository.GetCustomers();

        if (storedEntity == null)
            return null;

        var storedCustomers = storedEntity.Select(stored => _customerAdapter.FromCustomerEntity(stored)).ToList();

        return storedCustomers;
    }    
}
