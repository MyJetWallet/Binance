using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class LimitOrderTest
    {
        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new LimitOrder(user);

            ClassicAssert.Equal(OrderType.Limit, clientOrder.Type);
            ClassicAssert.Equal(0, clientOrder.Price);
            ClassicAssert.Equal(0, clientOrder.IcebergQuantity);
            ClassicAssert.Equal(TimeInForce.GTC, clientOrder.TimeInForce);
        }
    }
}
