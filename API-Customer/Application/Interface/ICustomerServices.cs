using Application.DTO;

namespace Application.Interface;

public interface ICustomerServices
{
    Task<CustomerResponse> Create(CustomerRequest request);
    Task<IEnumerable<CustomerResponse>> GetCustomers();
    Task<CustomerResponse> GetCustomerById(Guid id);
}
