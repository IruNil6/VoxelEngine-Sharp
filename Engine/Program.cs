﻿using EngineLibrary.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scope = GlobalServiceProvider.ServiceProvider.CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            
            logger.LogInformation("Hello, World!");
        }
    }
}
