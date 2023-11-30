using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Market
{
    public class SymbolPriceTest
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            const decimal value = 1.2345m;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new SymbolPrice(null, value));
            ClassicAssert.Throws<ArgumentException>("value", () => new SymbolPrice(symbol, -1));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            const decimal value = 1.2345m;

            var price = new SymbolPrice(symbol, value);

            ClassicAssert.Equal(symbol, price.Symbol);
            ClassicAssert.Equal(value, price.Value);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            const decimal value = 1.2345m;

            var price = new SymbolPrice(symbol, value);

            var json = JsonConvert.SerializeObject(price);

            price = JsonConvert.DeserializeObject<SymbolPrice>(json);

            ClassicAssert.Equal(symbol, price.Symbol);
            ClassicAssert.Equal(value, price.Value);
        }
    }
}
