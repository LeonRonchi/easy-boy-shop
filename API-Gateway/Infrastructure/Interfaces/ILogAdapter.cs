using Domain.Entities;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Interfaces;

public interface ILogAdapter
{
    public LogEntity ToLogEntity(Log log);

    public Log FromLogEntity(LogEntity logEntity);  
}
