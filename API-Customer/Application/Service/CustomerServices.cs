using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Validation;

namespace Application.Service;

public class CustomerServices : ICustomerServices
{
    private ICustomerRepository _customerRepository;
    private ICustomerMapper _customerMapper;

    public CustomerServices(ICustomerRepository customerRepository, ICustomerMapper customerMapper)
    {
        _customerRepository = customerRepository;
        _customerMapper = customerMapper;
    }

    public async Task<CustomerResponse> Create(CustomerRequest request)
    {
        var customer = _customerMapper.FromCustomerRequest(request);
        var stored = await _customerRepository.SaveAsync(customer);

        return _customerMapper.ToCustomerResponse(stored);
    }

    public async Task<CustomerResponse> GetCustomerById(Guid id)
    {
        var stored = await _customerRepository.GetCustomerByIdAsync(id);

        if (stored == null)
        {
            throw new NotFoundException(string.Format($"Recurso com ID {id} não foi encontrado."));
        }

        return _customerMapper.ToCustomerResponse(stored);
    }

    public async Task<IEnumerable<CustomerResponse>> GetCustomers()
    {
        var stored = await _customerRepository.GetCustomersAsync();

        if (!stored.Any())
        {
            return Enumerable.Empty<CustomerResponse>();
        }

        return stored.Select(log => _customerMapper.ToCustomerResponse(log)).ToList();
    }
}
