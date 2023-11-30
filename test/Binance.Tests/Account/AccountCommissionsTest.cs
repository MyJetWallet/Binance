using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Account
{
    public class AccountCommissionsTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentException>("maker", () => new AccountCommissions(-10, 10, 10, 10));
            ClassicAssert.Throws<ArgumentException>("maker", () => new AccountCommissions(10010, 10, 10, 10));

            ClassicAssert.Throws<ArgumentException>("taker", () => new AccountCommissions(10, -10, 10, 10));
            ClassicAssert.Throws<ArgumentException>("taker", () => new AccountCommissions(10, 10010, 10, 10));

            ClassicAssert.Throws<ArgumentException>("buyer", () => new AccountCommissions(10, 10, -10, 10));
            ClassicAssert.Throws<ArgumentException>("buyer", () => new AccountCommissions(10, 10, 10010, 10));

            ClassicAssert.Throws<ArgumentException>("seller", () => new AccountCommissions(10, 10, 10, -10));
            ClassicAssert.Throws<ArgumentException>("seller", () => new AccountCommissions(10, 10, 10, 10010));
        }

        [Fact]
        public void Properties()
        {
            var maker = 10;
            var taker = 20;
            var buyer = 30;
            var seller = 40;

            var commissions = new AccountCommissions(maker, taker, buyer, seller);

            ClassicAssert.Equal(maker, commissions.Maker);
            ClassicAssert.Equal(taker, commissions.Taker);
            ClassicAssert.Equal(buyer, commissions.Buyer);
            ClassicAssert.Equal(seller, commissions.Seller);
        }

        [Fact]
        public void Serialization()
        {
            var maker = 10;
            var taker = 20;
            var buyer = 30;
            var seller = 40;

            var commissions = new AccountCommissions(maker, taker, buyer, seller);

            var json = JsonConvert.SerializeObject(commissions);

            commissions = JsonConvert.DeserializeObject<AccountCommissions>(json);

            ClassicAssert.Equal(maker, commissions.Maker);
            ClassicAssert.Equal(taker, commissions.Taker);
            ClassicAssert.Equal(buyer, commissions.Buyer);
            ClassicAssert.Equal(seller, commissions.Seller);
        }
    }
}
