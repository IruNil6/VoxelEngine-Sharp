using Client.Common.Native;
using EngineLibrary.DI;
using EngineLibrary.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Client
{
    internal class Program
    {
        private static readonly ILogger Logger;
        private static readonly ClientOptions ClientOptions;

        static Program()
        {
            var scope = GlobalServiceProvider.ServiceProvider.CreateScope();
            Logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            ClientOptions = scope.ServiceProvider.GetRequiredService<IOptions<ClientOptions>>().Value;
        }

        [MTAThread]
        static void Main(string[] args)
        {
            Logger.LogInformation("Option Width: {0}\nOption Height: {1}", ClientOptions.Width, ClientOptions.Height);

            using var game = new GameWindowNative(ClientOptions.Width, ClientOptions.Height, ClientOptions.WindowState);
            game.Run();

        }
    }
}
