using Gateway.Domain.Entities;
using Gateway.Domain.Interfaces;
using Gateway.Infrastructure.Interfaces;
using System.Net.NetworkInformation;

namespace Infrastructure.Repository;

public class LogRepository : ILogRepository
{
    private readonly ILogScyllaRepository _logRepository;
    private readonly ILogAdapter _logAdapter;

    public LogRepository(ILogScyllaRepository logRepository, ILogAdapter logAdapter)
    {
        _logRepository = logRepository;
        _logAdapter = logAdapter;
    }

    public async Task<Log> SaveAsync(Log log)
    {
        var entity = _logAdapter.ToLogEntity(log);
        var storedEntity = await _logRepository.SaveAsync(entity);
        var storedLog = _logAdapter.FromLogEntity(storedEntity);

        return storedLog;
    }

    public Task<Log> GetLogByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Log>> GetLogsAsync()
    {
        var storedEntity = await _logRepository.GetLogs();

        if (storedEntity == null)
        {
            return Enumerable.Empty<Log>();
        }

        var storedLog = storedEntity.Select(stored => _logAdapter.FromLogEntity(stored)).ToList();


        return storedLog;
    }
}
