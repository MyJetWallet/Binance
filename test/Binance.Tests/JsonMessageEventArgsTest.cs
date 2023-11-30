using System;
using Xunit;

namespace Binance.Tests
{
    public class JsonMessageEventArgsTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentNullException>("json", () => new JsonMessageEventArgs(null));
            ClassicAssert.Throws<ArgumentNullException>("json", () => new JsonMessageEventArgs(string.Empty));
        }

        [Fact]
        public void Properties()
        {
            const string json = "{ }";
            const string subject = "unit-test";

            var args = new JsonMessageEventArgs(json, subject);

            ClassicAssert.Equal(json, args.Json);
            ClassicAssert.Equal(subject, args.Subject);
        }
    }
}
