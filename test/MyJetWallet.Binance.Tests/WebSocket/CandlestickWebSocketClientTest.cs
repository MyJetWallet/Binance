using System;
using System.Linq;
using MyJetWallet.Binance.Client;
using MyJetWallet.Binance.WebSocket;
using Xunit;

namespace MyJetWallet.Binance.Tests.WebSocket
{
    public class CandlestickWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var client = new CandlestickWebSocketClient();

            Assert.Throws<ArgumentNullException>("symbol", () => client.Subscribe((string)null, CandlestickInterval.Hour));
            Assert.Throws<ArgumentNullException>("symbol", () => client.Subscribe(string.Empty, CandlestickInterval.Hour));
        }

        [Fact]
        public void Subscribe()
        {
            var client = new CandlestickWebSocketClient();

            Assert.Empty(client.SubscribedStreams);
            Assert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(Symbol.BTC_USDT, CandlestickInterval.Hour);

            Assert.True(client.SubscribedStreams.Count() == 1);
            Assert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
