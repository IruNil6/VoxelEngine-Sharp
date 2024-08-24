using EngineLibrary.DI.Startup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EngineLibrary.DI
{
    public static class GlobalServiceProvider
    {
        public static readonly IConfiguration Configuration;
        public static readonly IServiceProvider ServiceProvider;

        static GlobalServiceProvider()
        {
            var runtimePath = Directory.GetCurrentDirectory();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(runtimePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            LoggingStartup.Configure(services, Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
