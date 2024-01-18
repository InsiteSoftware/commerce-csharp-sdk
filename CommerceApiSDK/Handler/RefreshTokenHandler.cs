using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommerceApiSDK.Models.Enums;
using CommerceApiSDK.Services;
using CommerceApiSDK.Services.Interfaces;

namespace CommerceApiSDK.Handler
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        private readonly Func<Task<bool>> renewAuthenticationTokensCallback;
        private readonly object refreshingTokenLock = new object();

        private readonly Action refreshTokenExpiredNotificationCallback;
        private readonly ILoggerService loggerService;

        public RefreshTokenHandler(
            HttpMessageHandler messageHandler,
            Func<Task<bool>> renewAuthenticationTokensCallback,
            ILoggerService loggerService,
            Action refreshTokenExpiredNotificationCallback
        )
            : base(messageHandler)
        {
            this.renewAuthenticationTokensCallback = renewAuthenticationTokensCallback;
            this.refreshTokenExpiredNotificationCallback = refreshTokenExpiredNotificationCallback;
            this.loggerService = loggerService;
        }

        private CancellationTokenSource GetCancellationTokenSource(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            TimeSpan timeout = request.GetTimeout();
            if (timeout == Timeout.InfiniteTimeSpan)
            {
                // No need to create a CTS if there's no timeout
                return null;
            }
            else
            {
                CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(
                    cancellationToken
                );
                cts.CancelAfter(timeout);
                return cts;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            loggerService.LogConsole(
                LogLevel.INFO,
                "Sending Async request : {0} {1}",
                request,
                cancellationToken
            );
            HttpResponseMessage result = null;

            using (
                CancellationTokenSource cts = GetCancellationTokenSource(request, cancellationToken)
            )
            {
                try
                {
                    result = await base.SendAsync(request, cts?.Token ?? cancellationToken);
                    loggerService.LogConsole(LogLevel.INFO, "SendAsync Response : {0}", result);
                }
                catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException();
                }
            }

            if (result.IsSuccessStatusCode)
            {
                return result;
            }

            if (
                request.RequestUri.AbsolutePath.Contains(CommerceAPIConstants.TokenUrl)
                || request.RequestUri.AbsolutePath.Contains(CommerceAPIConstants.TokenLogoutUrl)
            )
            {
                return result;
            }

            if (
                result.StatusCode == HttpStatusCode.Unauthorized
                || result.StatusCode == HttpStatusCode.Forbidden
            )
            {
                await Task.Run(() =>
                {
                    lock (refreshingTokenLock)
                    {
                        bool success = renewAuthenticationTokensCallback().Result;
                        if (!success)
                        {
                            refreshTokenExpiredNotificationCallback?.Invoke();
                        }
                    }
                });
            }

            return result;
        }
    }
}
