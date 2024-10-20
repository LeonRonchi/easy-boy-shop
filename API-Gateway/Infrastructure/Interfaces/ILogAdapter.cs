using Gateway.Domain.Entities;
using Gateway.Infrastructure.Repository.Entity;

namespace Gateway.Infrastructure.Interfaces;

public interface ILogAdapter
{
    public LogEntity ToLogEntity(Log log);

    public Log FromLogEntity(LogEntity logEntity);  
}
