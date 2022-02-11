using System;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DefaultLogger : ILoggerService
    {
        public void LogConsole(LogLevel level, string message)
        {
            string line = $"[{level}] : {message}";
            Console.WriteLine(line);
        }

        public void LogDebug(LogLevel level, string message)
        {
            string line = $"[{level}] : {message}";
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}
