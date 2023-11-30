using System;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class OrderTest
    {
        [Fact]
        public void Throws()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;
            const int id = 123456;
            const string clientOrderId = "test-order";
            const decimal price = 4999;
            const decimal originalQuantity = 1;
            const decimal executedQuantity = 0.5m;
            const decimal cummulativeQuoteAssetQuantity = executedQuantity * price;
            const OrderStatus status = OrderStatus.PartiallyFilled;
            const TimeInForce timeInForce = TimeInForce.IOC;
            const OrderType orderType = OrderType.Market;
            const OrderSide orderSide = OrderSide.Sell;
            const decimal stopPrice = 5000;
            const decimal icebergQuantity = 0.1m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const bool isWorking = true;

            ClassicAssert.Throws<ArgumentNullException>("user", () => new Order(null, symbol, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new Order(user, null, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("id", () => new Order(user, symbol, -1, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("price", () => new Order(user, symbol, id, clientOrderId, -1, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("stopPrice", () => new Order(user, symbol, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, -1, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("originalQuantity", () => new Order(user, symbol, id, clientOrderId, price, -1, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("executedQuantity", () => new Order(user, symbol, id, clientOrderId, price, originalQuantity, -1, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking));
            ClassicAssert.Throws<ArgumentException>("icebergQuantity", () => new Order(user, symbol, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, -1, time, time, isWorking));
        }

        [Fact]
        public void Properties()
        {
            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;
            const int id = 123456;
            const string clientOrderId = "test-order";
            const decimal price = 4999;
            const decimal originalQuantity = 1;
            const decimal executedQuantity = 0.5m;
            const decimal cummulativeQuoteAssetQuantity = executedQuantity * price;
            const OrderStatus status = OrderStatus.PartiallyFilled;
            const TimeInForce timeInForce = TimeInForce.IOC;
            const OrderType orderType = OrderType.Market;
            const OrderSide orderSide = OrderSide.Sell;
            const decimal stopPrice = 5000;
            const decimal icebergQuantity = 0.1m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const bool isWorking = true;

            var order = new Order(user, symbol, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking);

            ClassicAssert.Equal(user, order.User);
            ClassicAssert.Equal(symbol, order.Symbol);
            ClassicAssert.Equal(id, order.Id);
            ClassicAssert.Equal(clientOrderId, order.ClientOrderId);
            ClassicAssert.Equal(price, order.Price);
            ClassicAssert.Equal(originalQuantity, order.OriginalQuantity);
            ClassicAssert.Equal(executedQuantity, order.ExecutedQuantity);
            ClassicAssert.Equal(status, order.Status);
            ClassicAssert.Equal(timeInForce, order.TimeInForce);
            ClassicAssert.Equal(orderType, order.Type);
            ClassicAssert.Equal(orderSide, order.Side);
            ClassicAssert.Equal(stopPrice, order.StopPrice);
            ClassicAssert.Equal(icebergQuantity, order.IcebergQuantity);
            ClassicAssert.Equal(time, order.Time);
            ClassicAssert.Equal(isWorking, order.IsWorking);
        }
    }
}
