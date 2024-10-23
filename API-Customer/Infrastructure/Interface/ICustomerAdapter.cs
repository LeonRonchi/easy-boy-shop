using Domain.Model;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ICustomerAdapter
{
    public CustomerEntity ToCustomerEntity(Customer customer);
    public Customer FromCustomerEntity(CustomerEntity customerEntity);
}
