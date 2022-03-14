using System;
using System.Linq;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CommerceApiSDK.DemoApp.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        private const string KeyPrefix = "se-";

        private readonly IHttpContextAccessor httpContextAccessor;

        public SecureStorageService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Load(string key)
        {
            return this.httpContextAccessor.HttpContext?.Session.GetString(GetKey(key)) ?? string.Empty;
        }

        public bool Save(string key, string value)
        {
            try
            {
                this.httpContextAccessor.HttpContext?.Session.SetString(GetKey(key), value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(string key)
        {
            try
            {
                this.httpContextAccessor.HttpContext?.Session.Remove(GetKey(key));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ClearAll()
        {
            var success = true;
            foreach (var key in this.httpContextAccessor.HttpContext?.Session.Keys.Where(o => o.StartsWith(KeyPrefix)) ??
                Array.Empty<string>())
            {
                if (!this.Remove(key))
                {
                    success = false;
                }
            }

            return success;
        }

        private static string GetKey(string key) => $"{KeyPrefix}{key}";
    }
}
