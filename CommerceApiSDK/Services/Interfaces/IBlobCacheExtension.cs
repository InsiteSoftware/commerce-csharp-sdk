namespace CommerceApiSDK.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Akavache;

    public static class IBlobCacheExtension
    {
        public static async Task InvalidateObjectWithKeysStartingWith<T>(this IBlobCache blobCache, string keyPrefix)
        {
            var allKeys = await blobCache.GetAllKeys();
            var keysForInvalidating = new List<string>();

            foreach (var key in allKeys)
            {
                if (key.StartsWith(keyPrefix, StringComparison.Ordinal))
                {
                    keysForInvalidating.Add(key);
                }
            }

            if (keysForInvalidating.Count > 0)
            {
                await blobCache.InvalidateObjects<T>(keysForInvalidating);
            }
        }
    }
}
