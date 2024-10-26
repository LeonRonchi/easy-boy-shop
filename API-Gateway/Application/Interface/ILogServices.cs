using Application.DTO.Log;

namespace Application.Interfaces;

public interface ILogServices
{
    Task<LogResponse> Create(LogRequest log);
    Task<IEnumerable<LogResponse>> GetLogs();
    Task<LogResponse> GetLogById(int id);
}
