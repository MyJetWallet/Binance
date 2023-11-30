using System;
using Moq;
using Xunit;

namespace Binance.Tests.Account.Orders
{
    public class ClientOrderTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentNullException>("user", () => new TestClientOrder(null));
        }

        [Fact]
        public void Properties()
        {
            var user = new Mock<IBinanceApiUser>().Object;

            var clientOrder = new TestClientOrder(user);

            ClassicAssert.Equal(user, clientOrder.User);

            ClassicAssert.Null(clientOrder.Id);
            ClassicAssert.Null(clientOrder.Symbol);
            ClassicAssert.Null(clientOrder.Side);
            ClassicAssert.Equal(0, clientOrder.Quantity);
        }

        private class TestClientOrder : ClientOrder
        {
            public override OrderType Type => throw new NotImplementedException();

            public TestClientOrder(IBinanceApiUser user)
                : base(user)
            { }
        }
    }
}
