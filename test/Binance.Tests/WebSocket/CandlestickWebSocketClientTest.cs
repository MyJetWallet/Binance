using System;
using System.Linq;
using Binance.Client;
using Binance.WebSocket;
using Xunit;

namespace Binance.Tests.WebSocket
{
    public class CandlestickWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var client = new CandlestickWebSocketClient();

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => client.Subscribe((string)null, CandlestickInterval.Hour));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => client.Subscribe(string.Empty, CandlestickInterval.Hour));
        }

        [Fact]
        public void Subscribe()
        {
            var client = new CandlestickWebSocketClient();

            ClassicAssert.Empty(client.SubscribedStreams);
            ClassicAssert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(Symbol.BTC_USDT, CandlestickInterval.Hour);

            ClassicAssert.True(client.SubscribedStreams.Count() == 1);
            ClassicAssert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
