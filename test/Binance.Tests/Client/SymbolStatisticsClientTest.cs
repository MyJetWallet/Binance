using System.Linq;
using Binance.Client;
using Xunit;

namespace Binance.Tests.Client
{
    public class SymbolStatisticsClientTests
    {
        private readonly ISymbolStatisticsClient _client;

        public SymbolStatisticsClientTests()
        {
            _client = new SymbolStatisticsClient();
        }

        [Fact]
        public void Subscribe()
        {
            var symbol1 = Symbol.BTC_USDT;
            var symbol2 = Symbol.LTC_BTC;

            ClassicAssert.Empty(_client.SubscribedStreams);

            // Subscribe to symbol.
            _client.Subscribe(symbol1);
            ClassicAssert.Equal(SymbolStatisticsClient.GetStreamName(symbol1), _client.SubscribedStreams.Single());

            // Re-Subscribe to same symbol doesn't fail.
            _client.Subscribe(symbol1);
            ClassicAssert.Equal(SymbolStatisticsClient.GetStreamName(symbol1), _client.SubscribedStreams.Single());

            // Subscribe to a different symbol.
            _client.Subscribe(symbol2);
            ClassicAssert.True(_client.SubscribedStreams.Count() == 2);
            ClassicAssert.Contains(SymbolStatisticsClient.GetStreamName(symbol1), _client.SubscribedStreams);
            ClassicAssert.Contains(SymbolStatisticsClient.GetStreamName(symbol2), _client.SubscribedStreams);
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
