﻿using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Adapter.Interfaces;

public class LogAdapter : ILogAdapter
{
    public LogEntity ToLogEntity(Log log) => new LogEntity
    {
        Id = log.Id,
        ClientIP = log.ClientIP,
        Method = log.Method,
        StatusCode = log.StatusCode,
        Request = log.Request,
        Response = log.Response,
        Date = log.Date,
        Message = log.Message
    };

    public Log FromLogEntity(LogEntity logEntity) => new Log(
        logEntity.Id,
        logEntity.ClientIP,
        logEntity.Method,
        logEntity.Request,
        logEntity.Response,
        logEntity.Date,
        logEntity.StatusCode,
        logEntity.Message
    );
}
