using Gateway.Adapter.Interfaces;
using Gateway.Application.Interfaces;
using Gateway.Application.Mappings;
using Gateway.Application.Services;
using Gateway.Domain.Interfaces;
using Gateway.Infrastructure.Interfaces;
using Gateway.Infrastructure.Repository.Scylla;
using Infrastructure.Configuration;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // register services
        RegistryInfrastructureServices(builder);
        RegistryApplicationServices(builder);
        RegistryIncomingServices(builder);

        builder.Services.AddHttpClient();

        builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void RegistryInfrastructureServices(WebApplicationBuilder builder)
    {
        // Configuration contexts
        builder.Services.AddSingleton<IScyllaContext, ScyllaContext>();

        // Domain repositories
        builder.Services.AddSingleton<ILogRepository, LogRepository>();

        // ScyllaDB repositories
        builder.Services.AddSingleton<ILogScyllaRepository, LogScyllaRepository>();

        // Adapters
        builder.Services.AddSingleton<ILogAdapter, LogAdapter>();

    }

    private static void RegistryDomainServices(WebApplicationBuilder builder)
    {
        //builder.Services.AddSingleton<IGetMenuItems, GetMenuItems>();
    }

    private static void RegistryApplicationServices(WebApplicationBuilder builder)
    {
        // Mappers
        builder.Services.AddSingleton<ILogMapper, LogMapper>();

        // Services
        builder.Services.AddSingleton<ILogServices, LogServices>();
    }

    private static void RegistryIncomingServices(WebApplicationBuilder builder)
    {
        // CORS - Angular: localhost:4200
        #if DEBUG
        builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowLocalhost4200",
                        policy => policy.WithOrigins("http://localhost:4200")
                                        .AllowAnyMethod());

                });
        #endif
    }
}

    