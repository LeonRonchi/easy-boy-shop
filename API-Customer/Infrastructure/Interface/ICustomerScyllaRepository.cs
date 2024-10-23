using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ICustomerScyllaRepository
{
    Task<CustomerEntity> SaveAsync(CustomerEntity entity);
    Task<CustomerEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerEntity>> GetCustomers();
}
