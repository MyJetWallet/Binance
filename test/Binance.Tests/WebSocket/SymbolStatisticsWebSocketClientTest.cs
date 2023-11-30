using System;
using System.Linq;
using Binance.WebSocket;
using Binance.Client;
using Xunit;

namespace Binance.Tests.WebSocket
{
    public class SymbolStatisticsWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var client = new SymbolStatisticsWebSocketClient();

            ClassicAssert.Throws<ArgumentException>("symbols", () => client.Subscribe(null));
        }

        [Fact]
        public void Subscribe()
        {
            var client = new SymbolStatisticsWebSocketClient();

            ClassicAssert.Empty(client.SubscribedStreams);
            ClassicAssert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(Symbol.BTC_USDT);

            ClassicAssert.True(client.SubscribedStreams.Count() == 1);
            ClassicAssert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
