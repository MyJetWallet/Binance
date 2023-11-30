using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class AggregateTradeTests
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const decimal price = 5000;
            const decimal quantity = 1;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const bool isBuyerMaker = true;
            const bool isBestPriceMatch = true;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new AggregateTrade(null, id, price, quantity, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("id", () => new AggregateTrade(symbol, -1, price, quantity, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("price", () => new AggregateTrade(symbol, id, -1, quantity, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("quantity", () => new AggregateTrade(symbol, id, price, -1, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch));
            ClassicAssert.Throws<ArgumentException>("quantity", () => new AggregateTrade(symbol, id, price, 0, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("firstTradeId", () => new AggregateTrade(symbol, id, price, quantity, -1, lastTradeId, time, isBuyerMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("lastTradeId", () => new AggregateTrade(symbol, id, price, quantity, firstTradeId, -1, time, isBuyerMaker, isBestPriceMatch));
            ClassicAssert.Throws<ArgumentException>("lastTradeId", () => new AggregateTrade(symbol, id, price, quantity, lastTradeId, firstTradeId, time, isBuyerMaker, isBestPriceMatch));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const decimal price = 5000;
            const decimal quantity = 1;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const bool isBuyerMaker = true;
            const bool isBestPriceMatch = true;

            var trade = new AggregateTrade(symbol, id, price, quantity, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch);

            ClassicAssert.Equal(symbol, trade.Symbol);
            ClassicAssert.Equal(id, trade.Id);
            ClassicAssert.Equal(price, trade.Price);
            ClassicAssert.Equal(quantity, trade.Quantity);
            ClassicAssert.Equal(firstTradeId, trade.FirstTradeId);
            ClassicAssert.Equal(lastTradeId, trade.LastTradeId);
            ClassicAssert.Equal(time, trade.Time);
            ClassicAssert.Equal(isBuyerMaker, trade.IsBuyerMaker);
            ClassicAssert.Equal(isBestPriceMatch, trade.IsBestPriceMatch);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const decimal price = 5000;
            const decimal quantity = 1;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const long firstTradeId = 123456;
            const long lastTradeId = 234567;
            const bool isBuyerMaker = true;
            const bool isBestPriceMatch = true;

            var trade = new AggregateTrade(symbol, id, price, quantity, firstTradeId, lastTradeId, time, isBuyerMaker, isBestPriceMatch);

            var json = JsonConvert.SerializeObject(trade);

            trade = JsonConvert.DeserializeObject<AggregateTrade>(json);

            ClassicAssert.Equal(symbol, trade.Symbol);
            ClassicAssert.Equal(id, trade.Id);
            ClassicAssert.Equal(price, trade.Price);
            ClassicAssert.Equal(quantity, trade.Quantity);
            ClassicAssert.Equal(firstTradeId, trade.FirstTradeId);
            ClassicAssert.Equal(lastTradeId, trade.LastTradeId);
            ClassicAssert.Equal(time, trade.Time);
            ClassicAssert.Equal(isBuyerMaker, trade.IsBuyerMaker);
            ClassicAssert.Equal(isBestPriceMatch, trade.IsBestPriceMatch);
        }
    }
}
