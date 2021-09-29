using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace SwitEvent
{
    public static class EventSecurity
    {
        public const string SwitSecretVersion = "s0=";
        public const int SwitMaxDelay = 5; // minute

        public const string SwitRequestId = "X-Swit-Request-Id";
        public const string SwitSignature = "X-Swit-Signature";
        public const string SwitTimeStamp = "X-Swit-Request-Timestamp";

        // SwitSecrets include Timestamp and Signature for checking data is valid
        public class SwitSecrets
        {
            public string Timestamp { get; set; }
            public string Signature { get; set; }

            public SwitSecrets(string signature, string timestamp)
            {
                Signature = signature;
                Timestamp = timestamp;
            }
        }

        // GetSwitSecrets return SwitSecrets struct
        public static SwitSecrets GetSwitSecrets(NameValueCollection header)
        {
            string signature = header[SwitSignature];
            string stimestamp = header[SwitTimeStamp];

            if (signature == "")
            {
                return null;
            }

            if (stimestamp == "")
            {
                return null;
            }

            Console.WriteLine("{0}: {1}", SwitSignature, signature);
            Console.WriteLine("{0}: {1}", SwitTimeStamp, stimestamp);

            return new SwitSecrets(signature, stimestamp);
        }

        // ValidSwitSecrets reports whether message is valid
        public static bool ValidSwitSecrets(NameValueCollection header, string body, String secret)
        {
            SwitSecrets switSecrets = GetSwitSecrets(header);
            if (switSecrets == null)
            {
                throw new Exception("There is no signature or timestamp in request header");
            }

            Int64 timestamp = Int64.Parse(switSecrets.Timestamp);
            DateTime sent = EventUtils.UnixTimeStampToDateTime(timestamp);
            TimeSpan elapsedTime = DateTime.UtcNow.Subtract(sent);
            Console.WriteLine("Elapsed Time:{0}", elapsedTime.Seconds);
            if (Math.Abs(elapsedTime.Seconds) >= SwitMaxDelay * 60)
            {
                Console.WriteLine("Over max delay: {0} / {1}", elapsedTime.Seconds, SwitMaxDelay);
                return false;
            }

            string ssignature = EventUtils.TrimPrefix(SwitSecretVersion, switSecrets.Signature);
            byte[] storedHash = EventUtils.DecodeString(ssignature);

            bool err = false;
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                string prefix = string.Format("swit:{0}:{1}", switSecrets.Timestamp, body);
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(prefix));

                if (storedHash.Length == computedHash.Length)
                {
                    // compare the computed hash with the stored value
                    for (int i = 0; i < storedHash.Length; i++)
                    {
                        if (computedHash[i] != storedHash[i])
                        {
                            err = true;
                            break;
                        }
                    }
                }
            }

            if (err)
            {
                Console.WriteLine("Hash values differ");
                return false;
            }
            else
            {
                Console.WriteLine("Hash values agree");
                return true;
            }
        }
    }
}
