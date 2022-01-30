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
                this.stopwatch = Stopwatch.StartNew();
                this.isStarted = true;
            }
        }

        public void LogTime(string logMessage)
        {
            if (this.isStarted)
            {
                Logger.LogTrace("{this.message} {0} Time = {1}", logMessage, this.stopwatch.Elapsed.TotalMilliseconds);
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            if (this.isStarted)
            {
                this.stopwatch.Stop();
                this.isStarted = false;
                Logger.LogTrace("{0}  Time = {1}", this.message, this.stopwatch.Elapsed.TotalMilliseconds);
            }

            GC.SuppressFinalize(this);
        }
    }
}