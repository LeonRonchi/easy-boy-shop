using Domain.Entities;
using Application.DTO.Log;

namespace Application.Interfaces;

public interface ILogMapper
{
    public Log FromLogRequest(LogRequest LogResponse);
    public LogResponse ToLogResponse(Log Log);

}
