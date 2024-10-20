using Gateway.Domain.Entities;

namespace Gateway.Domain.Interfaces;

public interface ILogRepository
{
    Task<Log> SaveAsync(Log log);
    Task<IEnumerable<Log>> GetLogsAsync();
    Task<Log> GetLogByIdAsync(int id);
}
