using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class OrderBookPriceLevelTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentException>("price", () => new OrderBookPriceLevel(-1, 1));
            ClassicAssert.Throws<ArgumentException>("quantity", () => new OrderBookPriceLevel(1, -1));
        }

        [Fact]
        public void Zeroed()
        {
            const decimal price = 0;
            const decimal quantity = 0;

            var level = new OrderBookPriceLevel(price, quantity);

            ClassicAssert.Equal(price, level.Price);
            ClassicAssert.Equal(quantity, level.Quantity);
        }

        [Fact]
        public void Properties()
        {
            const decimal price = 0.123456789m;
            const decimal quantity = 0.987654321m;

            var level = new OrderBookPriceLevel(price, quantity);

            ClassicAssert.Equal(price, level.Price);
            ClassicAssert.Equal(quantity, level.Quantity);
        }

        [Fact]
        public void Serialization()
        {
            const decimal price = 0.123456789m;
            const decimal quantity = 0.987654321m;

            var level = new OrderBookPriceLevel(price, quantity);

            var json = JsonConvert.SerializeObject(level);

            level = JsonConvert.DeserializeObject<OrderBookPriceLevel>(json);

            ClassicAssert.Equal(price, level.Price);
            ClassicAssert.Equal(quantity, level.Quantity);
        }
    }
}
