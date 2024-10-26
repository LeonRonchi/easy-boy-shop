using Application.Dto.Customer;
using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using Application.Interface;

namespace Application.Mapper;

public class CustomerMapper : ICustomerMapper
{
    public CustomerResponse ToCustomerResponse(CustomerDto customerDto) => new()
    {
        Id = customerDto.Id,
        Name = customerDto.Name,
        Email = customerDto.Email,
        Cpf = customerDto.Cpf,
        BithDate = customerDto.BithDate
    };

    public CustomerDto ToCustomertDto(CustomerRequest request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Email = request.Email,
        Cpf = request.Cpf,
        BithDate = request.BithDate,
        RegisterDate = DateTime.UtcNow
    };
}
