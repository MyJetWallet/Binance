using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class StopLossLimitOrderTest
    {
        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new StopLossLimitOrder(user);

            ClassicAssert.Equal(OrderType.StopLossLimit, clientOrder.Type);
            ClassicAssert.Equal(0, clientOrder.StopPrice);
        }
    }
}
