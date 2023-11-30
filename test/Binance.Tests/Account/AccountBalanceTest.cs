using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Account
{
    public class AccountBalanceTest
    {
        [Fact]
        public void Throws()
        {
            var asset = Asset.BTC;
            const decimal free = 0.123m;
            const decimal locked = 0.456m;

            ClassicAssert.Throws<ArgumentNullException>("asset", () => new AccountBalance(null, free, locked));
            ClassicAssert.Throws<ArgumentException>("free", () => new AccountBalance(asset, -1, locked));
            ClassicAssert.Throws<ArgumentException>("locked", () => new AccountBalance(asset, free, -1));
        }

        [Fact]
        public void Properties()
        {
            var asset = Asset.BTC;
            const decimal free = 0.123m;
            const decimal locked = 0.456m;

            var balance = new AccountBalance(asset, free, locked);

            ClassicAssert.Equal(asset, balance.Asset);
            ClassicAssert.Equal(free, balance.Free);
            ClassicAssert.Equal(locked, balance.Locked);
        }

        [Fact]
        public void Serialization()
        {
            var asset = Asset.BTC;
            const decimal free = 0.123m;
            const decimal locked = 0.456m;

            var balance = new AccountBalance(asset, free, locked);

            var json = JsonConvert.SerializeObject(balance);

            balance = JsonConvert.DeserializeObject<AccountBalance>(json);

            ClassicAssert.Equal(asset, balance.Asset);
            ClassicAssert.Equal(free, balance.Free);
            ClassicAssert.Equal(locked, balance.Locked);
        }
    }
}
