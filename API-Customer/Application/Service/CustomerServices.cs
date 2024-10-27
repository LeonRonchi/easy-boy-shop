using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Exception;

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
        var stored = await _customerRepository.GetCustomerByIdAsync(id) ??
            throw new NotFoundException($"Cliente de ID {id} não encontrado");

        return _customerMapper.ToCustomerResponse(stored);
    }

    public async Task<IEnumerable<CustomerResponse>> GetCustomers()
    {
        var stored = await _customerRepository.GetCustomersAsync() ??
            throw new NotFoundException($"Nenhum Cliente encontrado");

        return stored.Select(log => _customerMapper.ToCustomerResponse(log)).ToList();
    }
}
