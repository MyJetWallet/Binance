using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Account
{
    public class AccountTradeTests
    {
        [Fact]
        public void Throws()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const long orderId = 54321;
            const decimal price = 5000;
            const decimal quantity = 1;
            const decimal quoteQuantity = price * quantity;
            const decimal commission = 10;
            var commissionAsset = Asset.BNB;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const bool isBuyer = true;
            const bool isMaker = true;
            const bool isBestPriceMatch = true;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new AccountTrade(null, id, orderId, price, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("id", () => new AccountTrade(symbol, -1, orderId, price, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));
            ClassicAssert.Throws<ArgumentException>("orderId", () => new AccountTrade(symbol, id, -1, price, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("price", () => new AccountTrade(symbol, id, orderId, -1, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));

            ClassicAssert.Throws<ArgumentException>("quantity", () => new AccountTrade(symbol, id, orderId, price, -1, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));
            ClassicAssert.Throws<ArgumentException>("quantity", () => new AccountTrade(symbol, id, orderId, price, 0, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch));
        }

        [Fact]
        public void Properties()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const long orderId = 54321;
            const decimal price = 5000;
            const decimal quantity = 1;
            const decimal quoteQuantity = price * quantity;
            const decimal commission = 10;
            var commissionAsset = Asset.BNB;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const bool isBuyer = true;
            const bool isMaker = true;
            const bool isBestPriceMatch = true;

            var trade = new AccountTrade(symbol, id, orderId, price, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch);

            ClassicAssert.Equal(symbol, trade.Symbol);
            ClassicAssert.Equal(id, trade.Id);
            ClassicAssert.Equal(orderId, trade.OrderId);
            ClassicAssert.Equal(price, trade.Price);
            ClassicAssert.Equal(quantity, trade.Quantity);
            ClassicAssert.Equal(quoteQuantity, trade.QuoteQuantity);
            ClassicAssert.Equal(commission, trade.Commission);
            ClassicAssert.Equal(commissionAsset, trade.CommissionAsset);
            ClassicAssert.Equal(time, trade.Time);
            ClassicAssert.Equal(isBuyer, trade.IsBuyer);
            ClassicAssert.Equal(isMaker, trade.IsMaker);
            ClassicAssert.Equal(isBestPriceMatch, trade.IsBestPriceMatch);
        }

        [Fact]
        public void Serialization()
        {
            var symbol = Symbol.BTC_USDT;
            const long id = 12345;
            const long orderId = 54321;
            const decimal price = 5000;
            const decimal quantity = 1;
            const decimal quoteQuantity = price * quantity;
            const decimal commission = 10;
            var commissionAsset = Asset.BNB;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const bool isBuyer = true;
            const bool isMaker = true;
            const bool isBestPriceMatch = true;

            var trade = new AccountTrade(symbol, id, orderId, price, quantity, quoteQuantity, commission, commissionAsset, time, isBuyer, isMaker, isBestPriceMatch);

            var json = JsonConvert.SerializeObject(trade);

            trade = JsonConvert.DeserializeObject<AccountTrade>(json);

            ClassicAssert.Equal(symbol, trade.Symbol);
            ClassicAssert.Equal(id, trade.Id);
            ClassicAssert.Equal(orderId, trade.OrderId);
            ClassicAssert.Equal(price, trade.Price);
            ClassicAssert.Equal(quantity, trade.Quantity);
            ClassicAssert.Equal(quoteQuantity, trade.QuoteQuantity);
            ClassicAssert.Equal(commission, trade.Commission);
            ClassicAssert.Equal(commissionAsset, trade.CommissionAsset);
            ClassicAssert.Equal(time, trade.Time);
            ClassicAssert.Equal(isBuyer, trade.IsBuyer);
            ClassicAssert.Equal(isMaker, trade.IsMaker);
            ClassicAssert.Equal(isBestPriceMatch, trade.IsBestPriceMatch);
        }
    }
}
