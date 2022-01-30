namespace CommerceApiSDK
{
    using System;

    public static class Console
    {
        public static void WriteLine(string message)
        {
            var timeStampedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff} CommerceApiSDK Diagnostic:   {message}";
            System.Diagnostics.Debug.WriteLine(timeStampedMessage);
        }
    }
}
