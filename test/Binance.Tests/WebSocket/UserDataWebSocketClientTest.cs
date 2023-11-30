using System;
using System.Linq;
using Binance.Client;
using Binance.WebSocket;
using Moq;
using Xunit;

namespace Binance.Tests.WebSocket
{
    public class UserDataWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var client = new UserDataWebSocketClient();

            ClassicAssert.Throws<ArgumentNullException>("listenKey", () => client.Subscribe(null, user));
            ClassicAssert.Throws<ArgumentNullException>("listenKey", () => client.Subscribe(string.Empty, user));
        }

        [Fact]
        public void Subscribe()
        {
            var listenKey = "<valid listen key>";
            var user = new Mock<IBinanceApiUser>().Object;
            var client = new UserDataWebSocketClient();

            ClassicAssert.Empty(client.SubscribedStreams);
            ClassicAssert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(listenKey, user);

            ClassicAssert.True(client.SubscribedStreams.Count() == 1);
            ClassicAssert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
