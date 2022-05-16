using System;

namespace CommerceApiSDK.Services.Messages
{
    public class AdminSignedOutOptiMessage : OptiMessage
    {
        public AdminSignedOutOptiMessage() { }

        public bool IsRefreshTokenExpired = false;
    }
}
