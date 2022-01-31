namespace CommerceApiSDK.Extensions
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class DataExtension
    {
        public static byte[] Serialize(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return null;
            }

            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
