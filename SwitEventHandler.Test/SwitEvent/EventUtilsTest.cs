using System;
using System.Text;

using Xunit;
using SwitEvent;

namespace SwitEventHandler.Test.SwitEvent
{
    public class EventUtilsTEst
    {
        [Fact]
        public void TrimPrefixTest()
        {
            string prefix = EventSecurity.SwitSecretVersion;
            string word = "test_word";
            string prefix_word = string.Format("{0}{1}", prefix, word);
            string trim_word = EventUtils.TrimPrefix(prefix, prefix_word);

            Assert.Equal(word, trim_word);
        }

        [Fact]
        public void EncodeStringTest()
        {
            string original_str = "abcdefghijklmnopqrstuvwxyz";
            string expected_str = "6162636465666768696a6b6c6d6e6f707172737475767778797a";
            string encode_str = EventUtils.EncodeString(original_str);

            Assert.Equal(expected_str, encode_str);
        }

        [Fact]
        public void DecodeStringTest()
        {
            string original_str = "6162636465666768696a6b6c6d6e6f707172737475767778797a";
            string expected_str = "abcdefghijklmnopqrstuvwxyz";
            byte[] decode_bytes = EventUtils.DecodeString(original_str);
            string decode_str = Encoding.UTF8.GetString(decode_bytes);

            Assert.Equal(expected_str, decode_str);
        }

        [Fact]
        public void UnixTimeStampToDateTimeTest()
        {
            DateTime expected_datetime = DateTime.Now.ToUniversalTime();
            double unit_time_stamp = ((DateTimeOffset)expected_datetime).ToUnixTimeSeconds();
            DateTime converted_dt = EventUtils.UnixTimeStampToDateTime(unit_time_stamp);
            TimeSpan diff = expected_datetime.Subtract(converted_dt);

            Assert.Equal(0, diff.Seconds);
        }
    }
}
