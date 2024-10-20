//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Refit;
//using Gateway.Infrastructure.Exceptions;
//using Gateway.Infrastructure.Extensions;

//namespace Gateway.Infrastructure.Extensions;

//public static class RefitClientExtensions
//{
//    public static IServiceCollection AddRefitClient<TClient>(this IServiceCollection services, string configKey) where TClient : class
//    {
//        services.AddRefitClient<TClient>()
//            .ConfigureHttpClient((serviceProvider, client) =>
//            {
//                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
//                var baseUrl = configuration.GetValue<string>(configKey);

//                if (string.IsNullOrEmpty(baseUrl))
//                    throw new InvalidConfigurationException($"{typeof(TClient).Name}: configuração inválida", $"Verifique a propriedade {configKey} em appsettings.json");

//                client.BaseAddress = new Uri(baseUrl);
//            });

//        return services;
//    }
//}