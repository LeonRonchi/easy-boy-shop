using Application.Dto.Customer;
using Application.DTO.Customer.Request;
using Refit;

namespace Outgoing.Http.Refit;

public interface ICustomerRefitClient
{
    [Get("/api/v1/customer/{id}")]
    Task<CustomerDto> GetCustomerByIdAsync(Guid? id);

    [Get("/api/v1/customer/all")]
    Task<IEnumerable<CustomerDto>> GetCustomerAllAsync();   

    [Post("/api/v1/customer")]
    Task<CustomerDto> CreateAsync([Body] CustomerRequest request);
}
