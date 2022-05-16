using System;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogDebug(LogLevel level, string message, params object[] formatparameters);

        void LogConsole(LogLevel level, string message, params object[] formatparameters);
    }
}
