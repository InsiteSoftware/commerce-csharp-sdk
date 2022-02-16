using System;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DefaultLogger : ILoggerService
    {
        public void LogConsole(LogLevel level, string message)
        {
            string line = $"Optimizely[{level}] : {message}";
            Console.WriteLine(line);
        }

        public void LogDebug(LogLevel level, string message)
        {
            string line = $"Optimizely[{level}] : {message}";
            System.Diagnostics.Debug.WriteLine(line);
        }

        public static void StaticConsole(LogLevel logMode, string msg )
        {
            DefaultLogger logger = new DefaultLogger();
            logger.LogConsole(logMode, msg);
        }

        public static void StaticDebug(LogLevel logMode, string msg)
        {
            DefaultLogger logger = new DefaultLogger();
            logger.LogDebug(logMode, msg);
        }

    }
}
