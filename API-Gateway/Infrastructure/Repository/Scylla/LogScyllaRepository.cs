using Cassandra;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Repository.Scylla;

public class LogScyllaRepository : ILogScyllaRepository
{
    private readonly IScyllaContext _context;

    public LogScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public Task<LogEntity> SaveAsync(LogEntity entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
    }

    public async Task<LogEntity?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM log_gateway WHERE id = ?";

        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToEntity(logRow);
    }

    public async Task<IEnumerable<LogEntity>> GetLogs()
    {
        var query = @"SELECT * FROM log_gateway";

        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return Enumerable.Empty<LogEntity>(); 

        return rows.Select(row => ToEntity(row)).ToList();
    }

    private async Task<LogEntity> Insert(LogEntity entity)
    {
        entity.Id = Guid.NewGuid();

        var operationTimestamp = DateTime.UtcNow;
        entity.CreatedAt = operationTimestamp;
        entity.UpdatedAt = operationTimestamp;

        var query = @"INSERT INTO log_gateway (
                        id, 
                        created_at, 
                        updated_at, 
                        client_ip,
                        method,
                        request,
                        response,
                        date,
                        status_code,
                        message
                    ) VALUES (?,?,?,?,?,?,?,?,?,?)";

        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.ClientIP,
            entity.Method,
            entity.Request,
            entity.Response,
            entity.Date,
            entity.StatusCode,
            entity.Message
         );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<LogEntity> Update(LogEntity entity)
    {
        var query = "UPDATE log_gateway SET updated_at=?, message=? WHERE id=?";
        entity.UpdatedAt = DateTime.UtcNow;

        var statement = new SimpleStatement(
           query,
           entity.UpdatedAt,
           entity.Message,
           entity.Id
        );

        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }

    private LogEntity ToEntity(Row row) => new LogEntity
    {
        Id = row.GetValue<Guid>("id"),
        CreatedAt = row.GetValue<DateTime>("created_at"),
        UpdatedAt = row.GetValue<DateTime>("updated_at"),
        ClientIP = row.GetValue<string>("client_ip"),
        Method = row.GetValue<string>("method"),
        Request = row.GetValue<string>("request"),
        Response = row.GetValue<string>("response"),
        Date = row.GetValue<DateTime>("date"),
        StatusCode = row.GetValue<string>("status_code"),
        Message = row.GetValue<string>("message")
    };
}
