using System;
using System.Linq;
using Binance.Cache;
using Binance.Client;
using Moq;
using Xunit;

namespace Binance.Tests.Cache
{
    public class CandlestickCacheTest
    {
        [Fact]
        public void SubscribeThrows()
        {
            var symbol = Symbol.BTC_USDT;
            var interval = CandlestickInterval.Day;
            var api = new Mock<IBinanceApi>().Object;
            var client = new Mock<ICandlestickClient>().Object;

            var cache = new CandlestickCache(api, client);

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => cache.Subscribe(null, CandlestickInterval.Day));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => cache.Subscribe(string.Empty, CandlestickInterval.Day));

            cache.Subscribe(symbol, interval);

            ClassicAssert.Throws<InvalidOperationException>(() => cache.Subscribe(symbol, interval));
        }

        [Fact]
        public void Unsubscribe()
        {
            var api = new Mock<IBinanceApi>().Object;
            var client = new Mock<ICandlestickClient>().Object;

            var cache = new CandlestickCache(api, client);

            // Can call unsubscribe before subscribe or multiple times without fail.
            cache.Unsubscribe();
            cache.Unsubscribe();
        }

        [Fact]
        public void LinkToClient()
        {
            var api = new Mock<IBinanceApi>().Object;
            var client1 = new Mock<ICandlestickClient>().Object;
            var client2 = new CandlestickClient();
            var symbol1 = Symbol.BTC_USDT;
            var symbol2 = Symbol.LTC_BTC;
            var interval = CandlestickInterval.Hour;

            client2.Subscribe(symbol1, interval);

            var clientSubscribeStreams = client2.SubscribedStreams.ToArray();
            ClassicAssert.Equal(CandlestickClient.GetStreamName(symbol1, interval), clientSubscribeStreams.Single());

            var cache = new CandlestickCache(api, client1)
            {
                Client = client2 // link client.
            };

            // Client subscribed streams are unchanged after link to unsubscribed cache.
            ClassicAssert.Equal(clientSubscribeStreams, client2.SubscribedStreams);

            cache.Client = client1; // unlink client.

            // Subscribe cache to symbol.
            cache.Subscribe(symbol2, interval);

            // Cache is subscribed to symbol.
            ClassicAssert.Equal(CandlestickClient.GetStreamName(symbol2, interval), cache.SubscribedStreams.Single());

            cache.Client = client2; // link to client.

            // Client has second subscribed stream from cache.
            ClassicAssert.True(client2.SubscribedStreams.Count() == 2);
            ClassicAssert.Contains(CandlestickClient.GetStreamName(symbol2, interval), client2.SubscribedStreams);
        }
    }
}
