using System;
using System.IO;
using MvvmCross;
using MvvmCross.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace CommerceApiSDK.Utils.Logger
{
    public class Logger
    {
        private const string DefaultLoggerName = "Default";

        private const string DefaultLogName = "logfile";

        private static IMvxLog currentLogger;

        public static bool IsAllLogsEnabled { get; private set; }

        public static IMvxLog CurrentLogger
        {
            get
            {
                if (currentLogger == null)
                {
                    currentLogger = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor(DefaultLoggerName);
                }

                return currentLogger;
            }
        }

        public static string GetLogText()
        {
            string logText = null;
            FileTarget fileTarget = GetLogTarget(DefaultLogName);
            if (fileTarget != null)
            {
                string logFilePath = Path.GetFullPath(fileTarget.FileName.Render(new LogEventInfo()));
                logText = File.ReadAllText(logFilePath);
            }

            return logText;
        }

        private static FileTarget GetLogTarget(string logName)
        {
            FileTarget fileTarget = null;
            Target logTarget = LogManager.Configuration.FindTargetByName(logName);
            if (logTarget != null)
            {
                fileTarget = logTarget as FileTarget;
                if (fileTarget == null)
                {
                    if (logTarget is AsyncTargetWrapper wrapTarget)
                    {
                        fileTarget = wrapTarget.WrappedTarget as FileTarget;
                    }
                }
            }

            return fileTarget;
        }

        public static void LogDebug(string message, params object[] formatParameters)
        {
            CurrentLogger?.Debug(message, formatParameters);
        }

        public static void LogTrace(string message, params object[] formatParameters)
        {
            CurrentLogger?.Trace(message, formatParameters);
        }

        public static void LogInfo(string message, params object[] formatParameters)
        {
            CurrentLogger?.Info(message, formatParameters);
        }

        public static void LogWarn(string message, params object[] formatParameters)
        {
            CurrentLogger.Warn(message, formatParameters);
        }

        public static void LogError(string message)
        {
            CurrentLogger?.Error(message);
        }

        public static void LogError(string message, params object[] formatParameters)
        {
            CurrentLogger?.Error(message, formatParameters);
        }

        public static void LogFatal(string message, params object[] formatParameters)
        {
            CurrentLogger?.Fatal(message, formatParameters);
        }

        public static void LogFatalException(string message, Exception exception, params object[] formatParameters)
        {
            CurrentLogger?.FatalException(message, exception, formatParameters);
        }

        public static void LogWarnException(string message, Exception exception, params object[] formatParameters)
        {
            CurrentLogger?.WarnException(message, exception, formatParameters);
        }

        public static void LogErrorException(string message, Exception exception, params object[] formatParameters)
        {
            CurrentLogger?.ErrorException(message, exception, formatParameters);
        }

        public static bool Log(
            MvxLogLevel logLevel,
            string message,
            Exception exception = null,
            params object[] formatParameters)
        {
            if (CurrentLogger == null)
            {
                Console.WriteLine("Current logger is null");
                return false;
            }

            return CurrentLogger.Log(logLevel, () => message, exception, formatParameters);
        }

        public static bool Log(MvxLogLevel logLevel, string message, params object[] formatParameters)
        {
            if (CurrentLogger == null)
            {
                Console.WriteLine("Current logger is null");
                return false;
            }

            return CurrentLogger.Log(logLevel, () => message, null, formatParameters);
        }

        public static bool IsLogLevelEnabled(MvxLogLevel logLevel)
        {
            if (CurrentLogger == null)
            {
                Console.WriteLine("Current logger is null");
                return false;
            }

            return CurrentLogger.IsLogLevelEnabled(logLevel);
        }

        public static void EnableAllLogs(bool enableLogs)
        {
            foreach (LoggingRule rule in LogManager.Configuration.LoggingRules)
            {
                if (enableLogs)
                {
                    rule.SetLoggingLevels(LogLevel.Trace, LogLevel.Fatal);
                }
                else
                {
                    rule.SetLoggingLevels(LogLevel.Error, LogLevel.Fatal);
                }
            }

            IsAllLogsEnabled = enableLogs;
            LogManager.Configuration.Reload();
            string logText = enableLogs ? "Logging is set Trace" : "Logging is set to Error";
            LogWarn(logText);
        }
    }
}