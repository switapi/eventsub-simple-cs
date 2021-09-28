using System;
using System.Text;

namespace SwitEvent
{
    public static class EventUtils
    {
        public static string TrimPrefix(string prefix, string word)
        {
            if (word.StartsWith(prefix) &&
                word.Length >= prefix.Length)
            {
                return word.Remove(0, prefix.Length);
            }

            return word;
        }

        public static string EncodeString(string str)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }

        public static byte[] DecodeString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime t = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return t.AddSeconds(unixTimeStamp).ToUniversalTime();
        }
    }
}
