using System;
using System.Linq;
using Binance.Client;
using Xunit;

namespace Binance.Tests.Client
{
    public class TradeClientTests
    {
        private readonly ITradeClient _client;

        public TradeClientTests()
        {
            _client = new TradeClient();
        }

        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => _client.Subscribe((string)null));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => _client.Subscribe(string.Empty));

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => _client.Unsubscribe((string)null));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => _client.Unsubscribe(string.Empty));
        }

        [Fact]
        public void Subscribe()
        {
            var symbol1 = Symbol.BTC_USDT;
            var symbol2 = Symbol.LTC_BTC;

            ClassicAssert.Empty(_client.SubscribedStreams);

            // Subscribe to symbol.
            _client.Subscribe(symbol1);
            ClassicAssert.Equal(TradeClient.GetStreamName(symbol1), _client.SubscribedStreams.Single());

            // Re-Subscribe to same symbol doesn't fail.
            _client.Subscribe(symbol1);
            ClassicAssert.Equal(TradeClient.GetStreamName(symbol1), _client.SubscribedStreams.Single());

            // Subscribe to a different symbol.
            _client.Subscribe(symbol2);
            ClassicAssert.True(_client.SubscribedStreams.Count() == 2);
            ClassicAssert.Contains(TradeClient.GetStreamName(symbol1), _client.SubscribedStreams);
            ClassicAssert.Contains(TradeClient.GetStreamName(symbol2), _client.SubscribedStreams);
        }

        [Fact]
        public void Unsubscribe()
        {
            var symbol = Symbol.BTC_USDT;

            ClassicAssert.Empty(_client.SubscribedStreams);

            // Unsubscribe non-subscribed symbol doesn't fail.
            _client.Unsubscribe(symbol);
            ClassicAssert.Empty(_client.SubscribedStreams);

            // Subscribe and unsubscribe symbol.
            _client.Subscribe(symbol).Unsubscribe(symbol);

            ClassicAssert.Empty(_client.SubscribedStreams);
        }

        [Fact]
        public void UnsubscribeAll()
        {
            var symbols = new string[] { Symbol.BTC_USDT, Symbol.ETH_USDT, Symbol.LTC_USDT };

            // Unsubscribe all when not subscribed doesn't fail.
            _client.Unsubscribe();

            // Subscribe to multiple symbols.
            _client.Subscribe(symbols);
            ClassicAssert.True(_client.SubscribedStreams.Count() == symbols.Length);

            // Unsubscribe all.
            _client.Unsubscribe();

            ClassicAssert.Empty(_client.SubscribedStreams);
        }
    }
}
