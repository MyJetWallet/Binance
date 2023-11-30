using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Binance.Api;

namespace Binance.Tests.Api
{
    [Collection("Binance HTTP Client Tests")]
    public class BinanceHttpClientExtensionsTest
    {
        private readonly IBinanceHttpClient _client;

        public BinanceHttpClientExtensionsTest()
        {
            // Configure services.
            var serviceProvider = new ServiceCollection()
                .AddBinance().BuildServiceProvider();

            // Get IBinanceHttpClient service.
            _client = serviceProvider.GetService<IBinanceHttpClient>();
        }

        #region Market Data

        [Fact]
        public Task GetOrderBookThrows()
        {
            return ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.GetOrderBookAsync(null));
        }

        [Fact]
        public async Task GetAggregateTradesThrows()
        {
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.GetAggregateTradesAsync(null));
        }

        [Fact]
        public Task GetCandlesticksThrows()
        {
            return ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.GetCandlesticksAsync(null, CandlestickInterval.Day));
        }

        #endregion Market Data

        #region Account

        [Fact]
        public async Task PlaceOrderThrows()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;
            var orderSide = OrderSide.Sell;
            var orderType = OrderType.Market;
            decimal quantity = 1;
            decimal price = 0;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.PlaceOrderAsync(null, symbol, orderSide, orderType, quantity, price));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.PlaceOrderAsync(user, null, orderSide, orderType, quantity, price));
            await ClassicAssert.ThrowsAsync<ArgumentException>("quantity", () => _client.PlaceOrderAsync(user, symbol, orderSide, orderType, -1, price));
        }

        [Fact]
        public async Task GetOrderThrows()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetOrderAsync(null, symbol));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.GetOrderAsync(user, null));
            await ClassicAssert.ThrowsAsync<ArgumentException>(() => _client.GetOrderAsync(user, symbol));
        }

        [Fact]
        public async Task CancelOrderThrows()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.CancelOrderAsync(null, symbol));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.CancelOrderAsync(user, null));
            await ClassicAssert.ThrowsAsync<ArgumentException>(() => _client.GetOrderAsync(user, symbol));
        }

        [Fact]
        public async Task GetOpenOrdersThrows()
        {
            var symbol = Symbol.BTC_USDT;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetOpenOrdersAsync(null, symbol));
        }

        [Fact]
        public async Task GetOrdersThrows()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetOrdersAsync(null, symbol));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("symbol", () => _client.GetOrdersAsync(user, null));
        }

        [Fact]
        public Task GetAccountThrows()
        {
            return ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetAccountInfoAsync(null));
        }

        [Fact]
        public Task GetTradesThrows()
        {
            var symbol = Symbol.BTC_USDT;

            return ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetAccountTradesAsync(null, symbol));
        }

        [Fact]
        public async Task WithdrawThrows()
        {
            var user = new BinanceApiUser("api-key");
            var asset = Asset.BTC;
            const string address = "12345678901234567890";
            const string addressTag = "ABCDEF";
            const decimal amount = 1;

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.WithdrawAsync(null, asset, address, addressTag, amount));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("asset", () => _client.WithdrawAsync(user, null, address, addressTag, amount));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("address", () => _client.WithdrawAsync(user, asset, null, addressTag, amount));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("address", () => _client.WithdrawAsync(user, asset, string.Empty, addressTag, amount));
            await ClassicAssert.ThrowsAsync<ArgumentException>("amount", () => _client.WithdrawAsync(user, asset, address, addressTag, -1));
            await ClassicAssert.ThrowsAsync<ArgumentException>("amount", () => _client.WithdrawAsync(user, asset, address, addressTag, 0));
        }

        [Fact]
        public Task GetDepositsThrows()
        {
            return ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetDepositsAsync(null));
        }

        [Fact]
        public Task GetWithdrawalsThrows()
        {
            return ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.GetWithdrawalsAsync(null));
        }

        #endregion Account

        #region User Stream

        [Fact]
        public async Task UserStreamStartThrows()
        {
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.UserStreamStartAsync((IBinanceApiUser)null));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("apiKey", () => _client.UserStreamStartAsync((string)null));
        }

        [Fact]
        public async Task UserStreamKeepAliveThrows()
        {
            var user = new BinanceApiUser("api-key");
            const string listenKey = "listen-key";

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.UserStreamKeepAliveAsync((IBinanceApiUser)null, listenKey));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("apiKey", () => _client.UserStreamKeepAliveAsync((string)null, listenKey));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("listenKey", () => _client.UserStreamKeepAliveAsync(user, null));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("listenKey", () => _client.UserStreamKeepAliveAsync(user, string.Empty));
        }

        [Fact]
        public async Task UserStreamCloseThrows()
        {
            var user = new BinanceApiUser("api-key");
            const string listenKey = "listen-key";

            await ClassicAssert.ThrowsAsync<ArgumentNullException>("user", () => _client.UserStreamCloseAsync((IBinanceApiUser)null, listenKey));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("apiKey", () => _client.UserStreamCloseAsync((string)null, listenKey));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("listenKey", () => _client.UserStreamCloseAsync(user, null));
            await ClassicAssert.ThrowsAsync<ArgumentNullException>("listenKey", () => _client.UserStreamCloseAsync(user, string.Empty));
        }

        #endregion User Stream
    }
}
