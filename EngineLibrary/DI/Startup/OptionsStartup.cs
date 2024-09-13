using EngineLibrary.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EngineLibrary.DI.Startup
{
    internal static class OptionsStartup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ClientOptions>(options => configuration.GetSection(nameof(ClientOptions)).Bind(options));
        }
    }
}
