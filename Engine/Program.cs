using Engine.Game;
using EngineLibrary.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {


            using var game = new GamePlatform(800, 600, "TestOpenTK");
            game.Run();
        }
    }
}
