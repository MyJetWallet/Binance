using System;
using Binance.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class CandlestickTest
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            const CandlestickInterval interval = CandlestickInterval.Hour;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const decimal open = 4950;
            const decimal high = 5100;
            const decimal low = 4900;
            const decimal close = 5050;
            const decimal volume = 1000;
            var closeTime = openTime.AddHours(1);
            const long quoteAssetVolume = 5000000;
            const int numberOfTrades = 555555;
            const decimal takerBuyBaseAssetVolume = 4444;
            const decimal takerBuyQuoteAssetVolume = 333;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new Candlestick(null, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));

            ClassicAssert.Throws<ArgumentException>("open", () => new Candlestick(symbol, interval, openTime, -1, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));
            ClassicAssert.Throws<ArgumentException>("high", () => new Candlestick(symbol, interval, openTime, open, -1, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));
            ClassicAssert.Throws<ArgumentException>("low", () => new Candlestick(symbol, interval, openTime, open, high, -1, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));
            ClassicAssert.Throws<ArgumentException>("close", () => new Candlestick(symbol, interval, openTime, open, high, low, -1, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));

            // HACK: https://api.binance.com/api/v1/klines?symbol=TRXBTC&interval=1M returns negative volume.
            //ClassicAssert.Throws<ArgumentException>("volume", () => new Candlestick(symbol, interval, openTime, open, high, low, close, -1, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));

            ClassicAssert.Throws<ArgumentException>("quoteAssetVolume", () => new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, -1, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));
            ClassicAssert.Throws<ArgumentException>("takerBuyBaseAssetVolume", () => new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, -1, takerBuyQuoteAssetVolume));
            ClassicAssert.Throws<ArgumentException>("takerBuyQuoteAssetVolume", () => new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, -1));

            ClassicAssert.Throws<ArgumentException>("numberOfTrades", () => new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, -1, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            const CandlestickInterval interval = CandlestickInterval.Hour;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const decimal open = 4950;
            const decimal high = 5100;
            const decimal low = 4900;
            const decimal close = 5050;
            const decimal volume = 1000;
            var closeTime = openTime.AddHours(1);
            const long quoteAssetVolume = 5000000;
            const int numberOfTrades = 555555;
            const decimal takerBuyBaseAssetVolume = 4444;
            const decimal takerBuyQuoteAssetVolume = 333;

            var candlestick = new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume);

            ClassicAssert.Equal(symbol, candlestick.Symbol);
            ClassicAssert.Equal(interval, candlestick.Interval);
            ClassicAssert.Equal(openTime, candlestick.OpenTime);
            ClassicAssert.Equal(open, candlestick.Open);
            ClassicAssert.Equal(high, candlestick.High);
            ClassicAssert.Equal(low, candlestick.Low);
            ClassicAssert.Equal(close, candlestick.Close);
            ClassicAssert.Equal(volume, candlestick.Volume);
            ClassicAssert.Equal(closeTime, candlestick.CloseTime);
            ClassicAssert.Equal(quoteAssetVolume, candlestick.QuoteAssetVolume);
            ClassicAssert.Equal(numberOfTrades, candlestick.NumberOfTrades);
            ClassicAssert.Equal(takerBuyBaseAssetVolume, candlestick.TakerBuyBaseAssetVolume);
            ClassicAssert.Equal(takerBuyQuoteAssetVolume, candlestick.TakerBuyQuoteAssetVolume);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            const CandlestickInterval interval = CandlestickInterval.Hour;
            var openTime = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const decimal open = 4950;
            const decimal high = 5100;
            const decimal low = 4900;
            const decimal close = 5050;
            const decimal volume = 1000;
            var closeTime = openTime.AddHours(1);
            const long quoteAssetVolume = 5000000;
            const int numberOfTrades = 555555;
            const decimal takerBuyBaseAssetVolume = 4444;
            const decimal takerBuyQuoteAssetVolume = 333;

            var candlestick = new Candlestick(symbol, interval, openTime, open, high, low, close, volume, closeTime, quoteAssetVolume, numberOfTrades, takerBuyBaseAssetVolume, takerBuyQuoteAssetVolume);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TimestampJsonConverter());

            var json = JsonConvert.SerializeObject(candlestick, settings);

            candlestick = JsonConvert.DeserializeObject<Candlestick>(json, settings);

            ClassicAssert.Equal(symbol, candlestick.Symbol);
            ClassicAssert.Equal(interval, candlestick.Interval);
            ClassicAssert.Equal(openTime, candlestick.OpenTime);
            ClassicAssert.Equal(open, candlestick.Open);
            ClassicAssert.Equal(high, candlestick.High);
            ClassicAssert.Equal(low, candlestick.Low);
            ClassicAssert.Equal(close, candlestick.Close);
            ClassicAssert.Equal(volume, candlestick.Volume);
            ClassicAssert.Equal(closeTime, candlestick.CloseTime);
            ClassicAssert.Equal(quoteAssetVolume, candlestick.QuoteAssetVolume);
            ClassicAssert.Equal(numberOfTrades, candlestick.NumberOfTrades);
            ClassicAssert.Equal(takerBuyBaseAssetVolume, candlestick.TakerBuyBaseAssetVolume);
            ClassicAssert.Equal(takerBuyQuoteAssetVolume, candlestick.TakerBuyQuoteAssetVolume);
        }
    }
}
