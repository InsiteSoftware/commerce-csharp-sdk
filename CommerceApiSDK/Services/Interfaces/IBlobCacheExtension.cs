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
            IEnumerable<string> allKeys = await blobCache.GetAllKeys();
            List<string> keysForInvalidating = new List<string>();

            foreach (string key in allKeys)
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
