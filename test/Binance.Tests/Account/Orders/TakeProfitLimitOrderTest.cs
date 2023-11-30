using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class TakeProfitLimitOrderTest
    {
        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new TakeProfitLimitOrder(user);

            ClassicAssert.Equal(OrderType.TakeProfitLimit, clientOrder.Type);
            ClassicAssert.Equal(0, clientOrder.StopPrice);
        }
    }
}
