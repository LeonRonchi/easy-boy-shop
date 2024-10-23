using Application.DTO;
using Domain.Model;

namespace Application.Interface;

public interface ICustomerMapper
{
    public Customer FromCustomerRequest(CustomerRequest CustomerResponse);

    public CustomerResponse ToCustomerResponse(Customer Customer);
}
