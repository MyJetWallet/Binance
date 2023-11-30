using System;
using Binance.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class OrderBookTest
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new OrderBook(null, lastUpdateId, bids, asks, DateTime.UtcNow));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new OrderBook("", lastUpdateId, bids, asks, DateTime.UtcNow));

            ClassicAssert.Throws<ArgumentException>("lastUpdateId", () => new OrderBook(symbol, -1, bids, asks, DateTime.UtcNow));
            ClassicAssert.Throws<ArgumentException>("lastUpdateId", () => new OrderBook(symbol, 0, bids, asks, DateTime.UtcNow));

            ClassicAssert.Throws<ArgumentNullException>("bids", () => new OrderBook(symbol, lastUpdateId, null, asks, DateTime.UtcNow));
            ClassicAssert.Throws<ArgumentNullException>("asks", () => new OrderBook(symbol, lastUpdateId, bids, null, DateTime.UtcNow));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(symbol, orderBook.Symbol);
            ClassicAssert.Equal(lastUpdateId, orderBook.LastUpdateId);

            ClassicAssert.NotEmpty(orderBook.Bids);
            ClassicAssert.NotEmpty(orderBook.Asks);

            ClassicAssert.Equal(3, orderBook.Top.Bid.Price);
            ClassicAssert.Equal(30, orderBook.Top.Bid.Quantity);

            ClassicAssert.Equal(4, orderBook.Top.Ask.Price);
            ClassicAssert.Equal(40, orderBook.Top.Ask.Quantity);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new OrderBookJsonConverter());

            var json = JsonConvert.SerializeObject(orderBook, settings);

            orderBook = JsonConvert.DeserializeObject<OrderBook>(json, settings);

            ClassicAssert.Equal(symbol, orderBook.Symbol);
            ClassicAssert.Equal(lastUpdateId, orderBook.LastUpdateId);

            ClassicAssert.NotEmpty(orderBook.Bids);
            ClassicAssert.NotEmpty(orderBook.Asks);

            ClassicAssert.Equal(3, orderBook.Top.Bid.Price);
            ClassicAssert.Equal(30, orderBook.Top.Bid.Quantity);

            ClassicAssert.Equal(4, orderBook.Top.Ask.Price);
            ClassicAssert.Equal(40, orderBook.Top.Ask.Quantity);
        }

        [Fact]
        public void Clone()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            var clone = orderBook.Clone();

            ClassicAssert.Equal(symbol, clone.Symbol);
            ClassicAssert.Equal(lastUpdateId, clone.LastUpdateId);

            foreach (var level in bids)
                ClassicAssert.Equal(level.Item2, clone.Quantity(level.Item1));

            foreach (var level in asks)
                ClassicAssert.Equal(level.Item2, clone.Quantity(level.Item1));

            ClassicAssert.Equal(3, orderBook.Top.Bid.Price);
            ClassicAssert.Equal(30, orderBook.Top.Bid.Quantity);

            ClassicAssert.Equal(4, orderBook.Top.Ask.Price);
            ClassicAssert.Equal(40, orderBook.Top.Ask.Quantity);

            ClassicAssert.Equal(orderBook, clone);
        }

        [Fact]
        public void Quantity()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            foreach (var level in bids)
                ClassicAssert.Equal(level.Item2, orderBook.Quantity(level.Item1));

            foreach (var level in asks)
                ClassicAssert.Equal(level.Item2, orderBook.Quantity(level.Item1));

            ClassicAssert.Equal(0, orderBook.Quantity(0.5m));
            ClassicAssert.Equal(0, orderBook.Quantity(10.0m));
        }

        [Fact]
        public void MidMarketPrice()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(3.5m, orderBook.MidMarketPrice());
        }

        [Fact]
        public void Depth()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(0, orderBook.Depth(3.5m));
            ClassicAssert.Equal(50, orderBook.Depth(1.5m));
            ClassicAssert.Equal(90, orderBook.Depth(5.5m));
        }

        [Fact]
        public void Volume()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(0, orderBook.Volume(3.5m));
            ClassicAssert.Equal(130, orderBook.Volume(1.5m)); // 3 * 30 + 2 * 20
            ClassicAssert.Equal(410, orderBook.Volume(5.5m)); // 4 * 40 + 5 * 50
        }

        [Fact]
        public void BidPrice()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(3, orderBook.Bids.PriceAt(15));
            ClassicAssert.Equal(2, orderBook.Bids.PriceAt(40));
            ClassicAssert.Equal(1, orderBook.Bids.PriceAt(55));
            ClassicAssert.Equal(1, orderBook.Bids.PriceAt(80));
        }

        [Fact]
        public void AskPrice()
        {
            var symbol = Symbol.BTC_USDT;
            const long lastUpdateId = 1234567890;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var orderBook = new OrderBook(symbol, lastUpdateId, bids, asks, DateTime.UtcNow);

            ClassicAssert.Equal(4, orderBook.Asks.PriceAt(20));
            ClassicAssert.Equal(5, orderBook.Asks.PriceAt(65));
            ClassicAssert.Equal(6, orderBook.Asks.PriceAt(120));
            ClassicAssert.Equal(6, orderBook.Asks.PriceAt(200));
        }
    }
}
