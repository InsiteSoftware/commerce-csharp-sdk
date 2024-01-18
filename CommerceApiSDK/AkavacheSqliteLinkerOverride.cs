// Note: This class file is *required* for iOS to work correctly, and is
// also a good idea for Android if you enable "Link All Assemblies".
using System;
using Akavache.Sqlite3;

namespace CommerceApiSDK
{
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            throw new Exception(typeof(SQLitePersistentBlobCache).FullName);
        }
    }

    public class PreserveAttribute : Attribute { }
}
