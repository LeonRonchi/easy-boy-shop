using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Entity;
using System.Xml.Linq;

namespace Infrastructure.Adapter;

public class CustomerAdapter : ICustomerAdapter
{
    public Customer FromCustomerEntity(CustomerEntity customerEntity) => new Customer(
        customerEntity.Id,
        customerEntity.Name,
        customerEntity.Email,
        customerEntity.Cpf,
        customerEntity.BithDate,
        customerEntity.RegisterDate
    );   

    public CustomerEntity ToCustomerEntity(Customer customer) => new()
    {
        Id = customer.Id,
        Name = customer.Name,
        Email = customer.Email,
        Cpf = customer.Cpf,
        BithDate = customer.BithDate,
        RegisterDate = customer.RegisterDate,
    };
    
}
