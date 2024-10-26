using Application.Dto.Customer;
using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;

namespace Application.Interface;

public interface ICustomerMapper
{
    CustomerDto ToCustomertDto(CustomerRequest request);
    CustomerResponse ToCustomerResponse(CustomerDto customerDto);
}
