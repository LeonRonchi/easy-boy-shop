using Cassandra;
using Gateway.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration;

public class ScyllaContext : IScyllaContext
{
    private readonly ISession _session;

    public ISession GetSession() => _session;

    public ScyllaContext(IConfiguration configuration)
    {
        var contactPoints = configuration
            .GetSection("Scylla:ContactPoints")
            .Get<string[]>();

        var port = configuration.GetValue<int>("Scylla:Port");

        var keyspace = configuration.GetValue<string>("Scylla:Keyspace");

        var cluster = Cluster.Builder()
            .AddContactPoints(contactPoints)
            .WithPort(port)
            .WithDefaultKeyspace(keyspace)
            .Build();

        _session = cluster.ConnectAndCreateDefaultKeyspaceIfNotExists();

        CreateTables();
    }

    private async void CreateTables() 
    {
        var createPacingQuery = @"CREATE TABLE IF NOT EXISTS log_gateway (
                            id UUID PRIMARY KEY,
                            created_at TIMESTAMP,
                            updated_at TIMESTAMP,
                            client_ip VARCHAR,
                            method VARCHAR,
                            request VARCHAR,
                            response VARCHAR,
                            date TIMESTAMP,
                            status_code VARCHAR,
                            message VARCHAR
                            )";

        await _session.ExecuteAsync(new SimpleStatement(createPacingQuery));
    }

}
