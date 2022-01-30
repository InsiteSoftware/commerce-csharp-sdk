﻿namespace CommerceApiSDK.Services.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// High level authentication api
    /// </summary>
    public interface IAuthenticationService
    {
        Task<(bool, ErrorResponse)> LogInAsync(string userName, string password);

        void Logout(bool isRefreshTokenExpired = false);

        Task LogoutAsync(bool isRefreshTokenExpired = false);

        Task<bool> IsAuthenticatedAsync();
    }
}