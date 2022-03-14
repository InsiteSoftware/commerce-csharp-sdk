using System;
using System.Linq;
using CommerceApiSDK.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CommerceApiSDK.DemoApp.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private const string KeyPrefix = "lo-";

        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalStorageService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Load(string key)
        {
            return this.httpContextAccessor.HttpContext?.Session.GetString(GetKey(key)) ?? string.Empty;
        }

        public string Load(string key, string defaultValue)
        {
            return this.httpContextAccessor.HttpContext?.Session.GetString(GetKey(key)) ?? defaultValue;
        }

        public int LoadInt(string key)
        {
            return this.httpContextAccessor.HttpContext?.Session.GetInt32(GetKey(key)) ?? -1;
        }

        public void Save(string key, string value)
        {
            this.httpContextAccessor.HttpContext?.Session.SetString(GetKey(key), value);
        }

        public void Save(string key, int value)
        {
            this.httpContextAccessor.HttpContext?.Session.SetInt32(GetKey(key), value);
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

        private static string GetKey(string key) => $"{KeyPrefix}{key}";
    }
}
