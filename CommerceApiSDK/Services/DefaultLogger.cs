using System;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Services
{
    public class DefaultLogger : ILoggerService
    {
        public void LogConsole(LogLevel level, string message, params object[] parameters)
        {
            string line = $"Optimizely[{level}] : {String.Format(message, parameters)}";
            Console.WriteLine(line);
        }

        public void LogDebug(LogLevel level, string message, params object[] parameters)
        {
            string line = $"Optimizely[{level}] : {String.Format(message, parameters)}";
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}
