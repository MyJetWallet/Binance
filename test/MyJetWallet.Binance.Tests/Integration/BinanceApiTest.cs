//#define INTEGRATION

#if INTEGRATION

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable PossibleMultipleEnumeration

namespace MyJetWallet.MyJetWallet.Binance.Tests.Integration
{
    public class BinanceApiTest
    {
        private readonly IBinanceApi _api;

        public BinanceApiTest()
        {
            // Configure services.
            var serviceProvider = new ServiceCollection()
                .AddBinance().BuildServiceProvider();

            // Get IBinanceApi service.
            _api = serviceProvider.GetService<IBinanceApi>();
        }

        #region Connectivity

        [Fact]
        public async Task Ping()
        {
            ClassicAssert.True(await _api.PingAsync());
        }

        [Fact]
        public async Task GetTimestamp()
        {
            var timestamp = await _api.GetTimestampAsync();

            ClassicAssert.True(timestamp > DateTimeOffset.UtcNow.AddSeconds(-30).ToUnixTimeMilliseconds());
        }

        [Fact]
        public async Task GetTime()
        {
            var time = await _api.GetTimeAsync();

            ClassicAssert.True(time > DateTime.UtcNow.AddSeconds(-30));
            ClassicAssert.Equal(DateTimeKind.Utc, time.Kind);
        }

        [Fact]
        public async Task GetRateLimitInfo()
        {
            var rateLimits = await _api.GetRateLimitInfoAsync();

            ClassicAssert.NotNull(rateLimits);
            ClassicAssert.NotEmpty(rateLimits);
        }

        #endregion Connectivity

        #region Market Data

        [Fact]
        public async Task GetOrderBook()
        {
            const int limit = 5;

            var orderBook = await _api.GetOrderBookAsync(Symbol.BTC_USDT, limit);

            ClassicAssert.NotNull(orderBook);
            ClassicAssert.NotEmpty(orderBook.Bids);
            ClassicAssert.NotEmpty(orderBook.Asks);
            ClassicAssert.True(orderBook.Bids.Count() == limit);
            ClassicAssert.True(orderBook.Asks.Count() == limit);
        }

        [Fact]
        public async Task GetAggregateTrades()
        {
            const int limit = 5;

            var trades = await _api.GetAggregateTradesAsync(Symbol.BTC_USDT, limit);

            ClassicAssert.NotNull(trades);
            ClassicAssert.NotEmpty(trades);
            ClassicAssert.True(trades.Count() == limit);
        }

        [Fact]
        public async Task GetAggregateTradesFrom()
        {
            const int fromId = 0;
            const int limit = 5;

            var trades = await _api.GetAggregateTradesFromAsync(Symbol.BTC_USDT, fromId, limit);

            ClassicAssert.NotNull(trades);
            ClassicAssert.NotEmpty(trades);
            ClassicAssert.True(trades.Count() == limit);
            ClassicAssert.True(trades.First().Id == fromId);
        }

        [Fact]
        public async Task GetAggregateTradesIn()
        {
            const int limit = 5;

            var limitTrades = await _api.GetAggregateTradesAsync(Symbol.BTC_USDT, limit);

            var startTime = limitTrades.First().Time;
            var endTime = limitTrades.Last().Time;

            var trades = await _api.GetAggregateTradesAsync(Symbol.BTC_USDT, startTime, endTime);

            ClassicAssert.NotNull(trades);
            ClassicAssert.NotEmpty(trades);
            ClassicAssert.True(trades.Count() >= limit);
            ClassicAssert.All(limitTrades, t1 => trades.Single(t2 => t2.Id == t1.Id));
        }

        [Fact]
        public async Task GetCandlesticks()
        {
            const int limit = 24;

            var candlesticks = await _api.GetCandlesticksAsync(Symbol.BTC_USDT, CandlestickInterval.Hour, limit);

            ClassicAssert.NotNull(candlesticks);
            ClassicAssert.NotEmpty(candlesticks);
            ClassicAssert.True(candlesticks.Count() == limit);
        }

        [Fact]
        public async Task GetCandlesticksIn()
        {
            const int limit = 24;

            var limitCandlesticks = await _api.GetCandlesticksAsync(Symbol.BTC_USDT, CandlestickInterval.Hour, limit);

            var startTime = limitCandlesticks.First().OpenTime;
            var endTime = limitCandlesticks.Last().OpenTime;
            const int newLimit = 12;

            var candlesticks = await _api.GetCandlesticksAsync(Symbol.BTC_USDT, CandlestickInterval.Hour, startTime, endTime, newLimit);

            ClassicAssert.NotNull(candlesticks);
            ClassicAssert.NotEmpty(candlesticks);
            ClassicAssert.True(candlesticks.Count() == newLimit);
            ClassicAssert.All(candlesticks, c1 => limitCandlesticks.Single(c2 => c2.OpenTime == c1.OpenTime));
        }

        [Fact]
        public async Task Get24HourStatistics()
        {
            var stats = await _api.Get24HourStatisticsAsync(Symbol.BTC_USDT);

            ClassicAssert.NotNull(stats);
        }

        [Fact]
        public async Task GetPrices()
        {
            var prices = await _api.GetPricesAsync();

            ClassicAssert.NotNull(prices);
            ClassicAssert.NotEmpty(prices);
        }

        [Fact]
        public async Task GetOrderBookTops()
        {
            var tops = await _api.GetOrderBookTopsAsync();

            ClassicAssert.NotNull(tops);
            ClassicAssert.NotEmpty(tops);
        }

        #endregion Market Data
    }
}

#endif
