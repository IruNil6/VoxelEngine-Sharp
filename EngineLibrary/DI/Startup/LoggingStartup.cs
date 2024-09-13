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
                    
                    .MinimumLevel
                    .Debug()
                    .WriteTo
                    .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}");

                var logger = loggerConfiguration.CreateLogger();
                builder.AddSerilog(logger);
            });
        }
    }
}
