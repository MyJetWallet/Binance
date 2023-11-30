using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class StopLossOrderTest
    {
        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new StopLossOrder(user);

            ClassicAssert.Equal(OrderType.StopLoss, clientOrder.Type);
            ClassicAssert.Equal(0, clientOrder.StopPrice);
        }
    }
}
