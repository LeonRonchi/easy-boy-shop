using Application.DTO;
using Application.Interface;
using Domain.Model;

namespace Application.Mapper;

public class CustomerMapper : ICustomerMapper
{
    public Customer FromCustomerRequest(CustomerRequest CustomerResponse) => new(
        CustomerResponse.Id,
        CustomerResponse.Name,
        CustomerResponse.Email,
        CustomerResponse.Cpf,
        CustomerResponse.BithDate
    );

    public CustomerResponse ToCustomerResponse(Customer Customer) => new()
    {
        Id = Customer.Id,
        Name = Customer.Name,
        Email = Customer.Email,
        Cpf = Customer.Cpf,
        BithDate = Customer.BithDate,
        RegisterDate = Customer.RegisterDate
    };
}
