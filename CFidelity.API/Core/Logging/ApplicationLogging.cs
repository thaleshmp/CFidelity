using Microsoft.Extensions.Logging;

namespace CFidelity.API.Core.Logging
{
    public static class ApplicationLogging
    {
        private static ILoggerFactory LoggerFactory { get; set; }

        public static void ConfigureLogger(ILoggerFactory factory)
        {
            factory.AddConsole();
            factory.AddAzureWebAppDiagnostics();
            factory.AddDebug();

            LoggerFactory = factory;
        }

        public static ILogger CreateLogger<T>() =>
          LoggerFactory.CreateLogger<T>();
    }
}
