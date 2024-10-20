using Gateway.Application.DTO.Log;

namespace Gateway.Application.Interfaces;

public interface ILogServices
{
    Task<LogResponse> Create(LogRequest log);
    Task<IEnumerable<LogResponse>> GetLogs();
    Task<LogResponse> GetLogById(int id);
}
