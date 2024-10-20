using Gateway.Infrastructure.Repository.Entity;

namespace Gateway.Infrastructure.Interfaces;

public interface ILogScyllaRepository
{
    Task<LogEntity> SaveAsync(LogEntity entity);
    Task<LogEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<LogEntity>> GetLogs();
}
