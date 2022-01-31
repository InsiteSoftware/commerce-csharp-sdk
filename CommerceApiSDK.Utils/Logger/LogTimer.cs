namespace CommerceApiSDK.Utils.Logger
{
    using System;
    using System.Diagnostics;
    using MvvmCross.Logging;

    public class LogTimer : IDisposable
    {
        public static bool IsTimerEnabled { get; set; } = true;
        private readonly string message;
        private readonly Stopwatch stopwatch;
        private bool isStarted;

        public LogTimer()
        {
        }

        public LogTimer(string message)
        {
            if (IsTimerEnabled && Logger.IsLogLevelEnabled(MvxLogLevel.Trace))
            {
                this.message = message;
                stopwatch = Stopwatch.StartNew();
                isStarted = true;
            }
        }

        public void LogTime(string logMessage)
        {
            if (isStarted)
            {
                Logger.LogTrace("{this.message} {0} Time = {1}", logMessage, stopwatch.Elapsed.TotalMilliseconds);
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            if (isStarted)
            {
                stopwatch.Stop();
                isStarted = false;
                Logger.LogTrace("{0}  Time = {1}", message, stopwatch.Elapsed.TotalMilliseconds);
            }

            GC.SuppressFinalize(this);
        }
    }
}