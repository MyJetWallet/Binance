using System;
using Xunit;

namespace Binance.Tests.Account
{
    public class DepositAddressTest
    {
        [Fact]
        public void Throws()
        {
            var asset = Asset.BTC;
            const string address = "1234567890";

            ClassicAssert.Throws<ArgumentNullException>("asset", () => new DepositAddress(null, address));
            ClassicAssert.Throws<ArgumentNullException>("address", () => new DepositAddress(asset, null));
        }

        [Fact]
        public void Properties()
        {
            var asset = Asset.BTC;
            const string address = "1234567890";
            const string addressTag = "12341234";

            var depositAddress = new DepositAddress(asset, address, addressTag);

            ClassicAssert.Equal(asset, depositAddress.Asset);
            ClassicAssert.Equal(address, depositAddress.Address);
            ClassicAssert.Equal(addressTag, depositAddress.AddressTag);
        }
    }
}
