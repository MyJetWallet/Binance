using System;
using System.Linq;
using MyJetWallet.Binance.Client;
using MyJetWallet.Binance.WebSocket;
using Xunit;

namespace MyJetWallet.Binance.Tests.WebSocket
{
    public class AggregateTradeWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var client = new AggregateTradeWebSocketClient();

            Assert.Throws<ArgumentNullException>("symbol", () => client.Subscribe((string)null));
            Assert.Throws<ArgumentNullException>("symbol", () => client.Subscribe(string.Empty));
        }

        [Fact]
        public void Subscribe()
        {
            var client = new AggregateTradeWebSocketClient();

            Assert.Empty(client.SubscribedStreams);
            Assert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(Symbol.BTC_USDT);

            Assert.True(client.SubscribedStreams.Count() == 1);
            Assert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
