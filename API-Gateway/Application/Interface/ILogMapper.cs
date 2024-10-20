using Gateway.Domain.Entities;
using Gateway.Application.DTO.Log;

namespace Gateway.Application.Interfaces;

public interface ILogMapper
{
    public Log FromLogRequest(LogRequest LogResponse);
    public LogResponse ToLogResponse(Log Log);

}
