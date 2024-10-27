using Cassandra;
using Infrastructure.Interface;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Repository.Scylla;

public class CustomerScyllaRepository : ICustomerScyllaRepository
{
    private readonly IScyllaContext _context;

    public CustomerScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public Task<CustomerEntity> SaveAsync(CustomerEntity entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
    }

    public async Task<CustomerEntity?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM customer WHERE id = ?";

        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToEntity(logRow);
    }

    public async  Task<IEnumerable<CustomerEntity>?> GetCustomers()
    {
        var query = @"SELECT * FROM customer";

        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToEntity(row)).ToList();
    }


    private async Task<CustomerEntity> Insert(CustomerEntity entity)
    {
        entity.Id = Guid.NewGuid();

        var operationTimestamp = DateTime.UtcNow;
        entity.CreatedAt = operationTimestamp;
        entity.UpdatedAt = operationTimestamp;

        var query = @"INSERT INTO customer (
                        id, 
                        created_at, 
                        updated_at, 
                        name,
                        email,
                        cpf,
                        bith_date,
                        register_date
                    ) VALUES (?,?,?,?,?,?,?,?)";

        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Name,
            entity.Email,
            entity.Cpf,
            entity.BithDate,
            entity.RegisterDate
         );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<CustomerEntity> Update(CustomerEntity entity)
    {
        var query = "UPDATE customer SET name=?, email=? WHERE id=?";
        entity.UpdatedAt = DateTime.UtcNow;

        var statement = new SimpleStatement(
           query,
           entity.Name,
           entity.Email,
           entity.Id
        );

        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }

    private CustomerEntity ToEntity(Row row) => new CustomerEntity
    {
        Id = row.GetValue<Guid>("id"),
        CreatedAt = row.GetValue<DateTime>("created_at"),
        UpdatedAt = row.GetValue<DateTime>("updated_at"),
        Name = row.GetValue<string>("name"),
        Email = row.GetValue<string>("email"),
        Cpf = row.GetValue<string>("cpf"),
        BithDate = row.GetValue<DateTime>("bith_date"),
        RegisterDate = row.GetValue<DateTime>("register_date")
    };
}
