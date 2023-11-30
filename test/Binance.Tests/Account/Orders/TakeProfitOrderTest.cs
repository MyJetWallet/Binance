using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class TakeProfitOrderTest
    {
        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new TakeProfitOrder(user);

            ClassicAssert.Equal(OrderType.TakeProfit, clientOrder.Type);
            ClassicAssert.Equal(0, clientOrder.StopPrice);
        }
    }
}
