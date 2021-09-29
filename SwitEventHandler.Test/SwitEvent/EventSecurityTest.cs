using System;
using System.IO;
using System.Collections.Specialized;

using Xunit;
using SwitEvent;

namespace SwitEventHandler.Test.SwitEvent
{
    public static class EventSecurityTest
    {
        public const string SwiteventTestBodyJson = "switevent_test_body.json";
        public const string SwiteventSecret = "O0Qyog0ARKFdGuts8hXdbISvTMCEtest";

        public static string GetBodyString()
        {
            string prj_path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(prj_path, "SwitEvent", EventSecurityTest.SwiteventTestBodyJson);
            return File.ReadAllText(path);
        }

        [Fact]
        public static void ValidSwitSecretsTest()
        {
            var body = GetBodyString();
            var timestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            var signature = EventUtils.GetHMACSignature(body, SwiteventSecret, timestamp);
            Assert.NotEmpty(signature);

            NameValueCollection header = new NameValueCollection();
            header.Add(EventSecurity.SwitSignature, string.Format("{0}{1}", EventSecurity.SwitSecretVersion, signature));
            header.Add(EventSecurity.SwitTimeStamp, timestamp);

            var result = EventSecurity.ValidSwitSecrets(header, body, SwiteventSecret);
            Assert.True(result);
        }
    }
}
