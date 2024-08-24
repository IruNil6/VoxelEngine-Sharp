using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace EngineLibrary.DI.Startup
{
    internal static class LoggingStartup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            //TODO: Maybe in future add elasticsearch or something
            services.AddLogging(builder =>
            {
                var loggerConfiguration = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration);

                var logger = loggerConfiguration.CreateLogger();
                builder.AddSerilog(logger);
            });
        }
    }
}
