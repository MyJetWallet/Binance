﻿using System;
using Binance.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class SymbolStatisticsTest
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            var period = TimeSpan.FromHours(24);
            const decimal priceChange = 50;
            const decimal priceChangePercent = 1;
            const decimal weightedAveragePrice = 5001;
            const decimal previousClosePrice = 4900;
            const decimal lastPrice = 5000;
            const decimal lastQuantity = 1;
            const decimal bidPrice = 4995;
            const decimal bidQuantity = 2;
            const decimal askPrice = 5005;
            const decimal askQuantity = 3;
            const decimal openPrice = 4950;
            const decimal highPrice = 5025;
            const decimal lowPrice = 4925;
            const decimal volume = 100000;
            const decimal quoteVolume = 200000;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var closeTime = openTime.AddHours(24);
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const long tradeCount = lastTradeId - firstTradeId + 1;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new SymbolStatistics(null, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));

            // HACK
            //ClassicAssert.Throws<ArgumentException>("weightedAveragePrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, -1, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("previousClosePrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, -1, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("lastPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, -1, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("lastQuantity", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, -1, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));

            ClassicAssert.Throws<ArgumentException>("bidPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, -1, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("bidQuantity", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, -1, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("askPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, -1, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("askQuantity", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, -1, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));

            ClassicAssert.Throws<ArgumentException>("openPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, -1, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("highPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, -1, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("lowPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, -1, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("lowPrice", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, lowPrice, highPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));

            // HACK
            //ClassicAssert.Throws<ArgumentException>("volume", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, -1, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));
            ClassicAssert.Throws<ArgumentException>("quoteVolume", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, -1, openTime, closeTime, firstTradeId, lastTradeId, tradeCount));

            ClassicAssert.Throws<ArgumentException>("openTime", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, closeTime, openTime, firstTradeId, lastTradeId, tradeCount));

            ClassicAssert.Throws<ArgumentException>("lastTradeId", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, lastTradeId, firstTradeId, tradeCount));

            ClassicAssert.Throws<ArgumentException>("tradeCount", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, -1));

            // TODO
            //ClassicAssert.Throws<ArgumentException>("tradeCount", () => new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount + 1));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            var period = TimeSpan.FromHours(24);
            const decimal priceChange = 50;
            const decimal priceChangePercent = 1;
            const decimal weightedAveragePrice = 5001;
            const decimal previousClosePrice = 4900;
            const decimal lastPrice = 5000;
            const decimal lastQuantity = 1;
            const decimal bidPrice = 4995;
            const decimal bidQuantity = 2;
            const decimal askPrice = 5005;
            const decimal askQuantity = 3;
            const decimal openPrice = 4950;
            const decimal highPrice = 5025;
            const decimal lowPrice = 4925;
            const decimal volume = 100000;
            const decimal quoteVolume = 200000;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var closeTime = openTime.AddHours(24);
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const long tradeCount = lastTradeId - firstTradeId + 1;

            var stats = new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount);

            ClassicAssert.Equal(symbol, stats.Symbol);
            ClassicAssert.Equal(priceChange, stats.PriceChange);
            ClassicAssert.Equal(priceChangePercent, stats.PriceChangePercent);
            ClassicAssert.Equal(weightedAveragePrice, stats.WeightedAveragePrice);
            ClassicAssert.Equal(previousClosePrice, stats.PreviousClosePrice);
            ClassicAssert.Equal(lastPrice, stats.LastPrice);
            ClassicAssert.Equal(lastQuantity, stats.LastQuantity);
            ClassicAssert.Equal(bidPrice, stats.BidPrice);
            ClassicAssert.Equal(bidQuantity, stats.BidQuantity);
            ClassicAssert.Equal(askPrice, stats.AskPrice);
            ClassicAssert.Equal(askQuantity, stats.AskQuantity);
            ClassicAssert.Equal(openPrice, stats.OpenPrice);
            ClassicAssert.Equal(highPrice, stats.HighPrice);
            ClassicAssert.Equal(lowPrice, stats.LowPrice);
            ClassicAssert.Equal(volume, stats.Volume);
            ClassicAssert.Equal(quoteVolume, stats.QuoteVolume);
            ClassicAssert.Equal(openTime, stats.OpenTime);
            ClassicAssert.Equal(closeTime, stats.CloseTime);
            ClassicAssert.Equal(firstTradeId, stats.FirstTradeId);
            ClassicAssert.Equal(lastTradeId, stats.LastTradeId);
            ClassicAssert.Equal(tradeCount, stats.TradeCount);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            var period = TimeSpan.FromHours(24);
            const decimal priceChange = 50;
            const decimal priceChangePercent = 1;
            const decimal weightedAveragePrice = 5001;
            const decimal previousClosePrice = 4900;
            const decimal lastPrice = 5000;
            const decimal lastQuantity = 1;
            const decimal bidPrice = 4995;
            const decimal bidQuantity = 2;
            const decimal askPrice = 5005;
            const decimal askQuantity = 3;
            const decimal openPrice = 4950;
            const decimal highPrice = 5025;
            const decimal lowPrice = 4925;
            const decimal volume = 100000;
            const decimal quoteVolume = 200000;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var closeTime = openTime.AddHours(24);
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const long tradeCount = lastTradeId - firstTradeId + 1;

            var stats = new SymbolStatistics(symbol, period, priceChange, priceChangePercent, weightedAveragePrice, previousClosePrice, lastPrice, lastQuantity, bidPrice, bidQuantity, askPrice, askQuantity, openPrice, highPrice, lowPrice, volume, quoteVolume, openTime, closeTime, firstTradeId, lastTradeId, tradeCount);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TimestampJsonConverter());

            var json = JsonConvert.SerializeObject(stats, settings);

            stats = JsonConvert.DeserializeObject<SymbolStatistics>(json, settings);

            ClassicAssert.Equal(symbol, stats.Symbol);
            ClassicAssert.Equal(priceChange, stats.PriceChange);
            ClassicAssert.Equal(priceChangePercent, stats.PriceChangePercent);
            ClassicAssert.Equal(weightedAveragePrice, stats.WeightedAveragePrice);
            ClassicAssert.Equal(previousClosePrice, stats.PreviousClosePrice);
            ClassicAssert.Equal(lastPrice, stats.LastPrice);
            ClassicAssert.Equal(lastQuantity, stats.LastQuantity);
            ClassicAssert.Equal(bidPrice, stats.BidPrice);
            ClassicAssert.Equal(bidQuantity, stats.BidQuantity);
            ClassicAssert.Equal(askPrice, stats.AskPrice);
            ClassicAssert.Equal(askQuantity, stats.AskQuantity);
            ClassicAssert.Equal(openPrice, stats.OpenPrice);
            ClassicAssert.Equal(highPrice, stats.HighPrice);
            ClassicAssert.Equal(lowPrice, stats.LowPrice);
            ClassicAssert.Equal(volume, stats.Volume);
            ClassicAssert.Equal(quoteVolume, stats.QuoteVolume);
            ClassicAssert.Equal(openTime, stats.OpenTime);
            ClassicAssert.Equal(closeTime, stats.CloseTime);
            ClassicAssert.Equal(firstTradeId, stats.FirstTradeId);
            ClassicAssert.Equal(lastTradeId, stats.LastTradeId);
            ClassicAssert.Equal(tradeCount, stats.TradeCount);
        }
    }
}
