using Cassandra;

namespace Gateway.Infrastructure.Interfaces;

public interface IScyllaContext
{
    ISession GetSession();
}
