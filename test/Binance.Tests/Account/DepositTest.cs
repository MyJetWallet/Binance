using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Account
{
    public class DepositTest
    {
        [Fact]
        public void Throws()
        {
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const DepositStatus status = DepositStatus.Success;
            const string address = "0x12345678901234567890";

            ClassicAssert.Throws<ArgumentNullException>("asset", () => new Deposit(null, amount, time, status, address));
            ClassicAssert.Throws<ArgumentException>("amount", () => new Deposit(asset, -1, time, status, address));
            ClassicAssert.Throws<ArgumentException>("amount", () => new Deposit(asset, 0, time, status, address));
            ClassicAssert.Throws<ArgumentNullException>("address", () => new Deposit(asset, amount, time, status, null));
            ClassicAssert.Throws<ArgumentNullException>("address", () => new Deposit(asset, amount, time, status, string.Empty));
        }

        [Fact]
        public void Properties()
        {
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const DepositStatus status = DepositStatus.Success;
            const string address = "0x12345678901234567890";
            const string addressTag = "ABCDEF";
            const string txId = "21436587092143658709";

            var deposit = new Deposit(asset, amount, time, status, address, addressTag, txId);

            ClassicAssert.Equal(asset, deposit.Asset);
            ClassicAssert.Equal(amount, deposit.Amount);
            ClassicAssert.Equal(time, deposit.Time);
            ClassicAssert.Equal(status, deposit.Status);
            ClassicAssert.Equal(address, deposit.Address);
            ClassicAssert.Equal(addressTag, deposit.AddressTag);
            ClassicAssert.Equal(txId, deposit.TxId);
        }

        [Fact]
        public void Serialization()
        {
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const DepositStatus status = DepositStatus.Success;
            const string address = "0x12345678901234567890";
            const string addressTag = "ABCDEF";
            const string txId = "21436587092143658709";

            var deposit = new Deposit(asset, amount, time, status, address, addressTag, txId);

            var json = JsonConvert.SerializeObject(deposit);

            deposit = JsonConvert.DeserializeObject<Deposit>(json);

            ClassicAssert.Equal(asset, deposit.Asset);
            ClassicAssert.Equal(amount, deposit.Amount);
            ClassicAssert.Equal(time, deposit.Time);
            ClassicAssert.Equal(status, deposit.Status);
            ClassicAssert.Equal(address, deposit.Address);
            ClassicAssert.Equal(addressTag, deposit.AddressTag);
            ClassicAssert.Equal(txId, deposit.TxId);
        }
    }
}
