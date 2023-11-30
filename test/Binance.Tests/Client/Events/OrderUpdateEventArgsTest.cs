using System;
using Binance.Client;
using Xunit;

namespace Binance.Tests.Client.Events
{
    public class OrderUpdateEventArgsTest
    {
        [Fact]
        public void Throws()
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;

            const OrderExecutionType orderExecutionType = OrderExecutionType.New;
            const string orderRejectedReason = OrderRejectedReason.None;
            const string newClientOrderId = "new-test-order";

            ClassicAssert.Throws<ArgumentNullException>("order", () => new OrderUpdateEventArgs(time, null, orderExecutionType, orderRejectedReason, newClientOrderId));
        }

        [Fact]
        public void Properties()
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;

            var user = new BinanceApiUser("api-key");
            var symbol = Symbol.BTC_USDT;
            const long id = 123456;
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
            const bool isWorking = true;

            var order = new Order(user, symbol, id, clientOrderId, price, originalQuantity, executedQuantity, cummulativeQuoteAssetQuantity, status, timeInForce, orderType, orderSide, stopPrice, icebergQuantity, time, time, isWorking);

            const OrderExecutionType orderExecutionType = OrderExecutionType.New;
            const string orderRejectedReason = OrderRejectedReason.None;
            const string newClientOrderId = "new-test-order";

            var args = new OrderUpdateEventArgs(time, order, orderExecutionType, orderRejectedReason, newClientOrderId);

            ClassicAssert.Equal(time, args.Time);
            ClassicAssert.Equal(order, args.Order);
            ClassicAssert.Equal(orderExecutionType, args.OrderExecutionType);
            ClassicAssert.Equal(orderRejectedReason, args.OrderRejectedReason);
            ClassicAssert.Equal(newClientOrderId, args.NewClientOrderId);
        }
    }
}
