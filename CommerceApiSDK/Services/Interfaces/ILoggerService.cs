using System;
using CommerceApiSDK.Models.Enums;

namespace CommerceApiSDK.Services.Interfaces
{
    public interface ILoggerService
    {
        void LogDebug(LogLevel level, string message);

        void LogConsole(LogLevel level, string message);

    }
}
