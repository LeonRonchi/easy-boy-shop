using Application.DTO.Log;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class LogServices : ILogServices
{
    private ILogRepository _logRepository;
    private ILogMapper _logMapper;

    public LogServices(ILogRepository logRepository, ILogMapper logMapper)
    {
        _logRepository = logRepository;
        _logMapper = logMapper;
    }

    public async Task<LogResponse> Create(LogRequest request)
    {
        var log = _logMapper.FromLogRequest(request);
        var stored = await _logRepository.SaveAsync(log);

        return _logMapper.ToLogResponse(stored);
    }

    public async Task<LogResponse> GetLogById(int id)
    {
        var response = await _logRepository.GetLogByIdAsync(id);

        return _logMapper.ToLogResponse(response);
    }

    public async Task<IEnumerable<LogResponse>> GetLogs()
    {
        var stored = await _logRepository.GetLogsAsync();

        if (!stored.Any())
        {
            return Enumerable.Empty<LogResponse>();
        }

        var temp = stored.Select(log => _logMapper.ToLogResponse(log)).ToList();

        return temp;
    }
}
