using Domain.Entities;

namespace Domain.Interfaces;

public interface ILogRepository
{
    Task<Log> SaveAsync(Log log);
    Task<IEnumerable<Log>> GetLogsAsync();
    Task<Log> GetLogByIdAsync(int id);
}
