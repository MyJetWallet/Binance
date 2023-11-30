﻿using System;
using System.Linq;
using Binance.Client;
using Binance.WebSocket;
using Xunit;

namespace Binance.Tests.WebSocket
{
    public class TradesWebSocketClientTest
    {
        [Fact]
        public void Throws()
        {
            var client = new TradeWebSocketClient();

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => client.Subscribe((string)null));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => client.Subscribe(string.Empty));
        }

        [Fact]
        public void Subscribe()
        {
            var client = new TradeWebSocketClient();

            ClassicAssert.Empty(client.SubscribedStreams);
            ClassicAssert.Empty(client.Publisher.PublishedStreams);

            client.Subscribe(Symbol.BTC_USDT);

            ClassicAssert.True(client.SubscribedStreams.Count() == 1);
            ClassicAssert.True(client.Publisher.PublishedStreams.Count() == 1);
        }
    }
}
