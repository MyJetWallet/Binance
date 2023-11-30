using System;
using Newtonsoft.Json;
using Xunit;

namespace Binance.Tests.Account
{
    public class WithdrawalTest
    {
        [Fact]
        public void Throws()
        {
            const string id = "1234567890";
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const WithdrawalStatus status = WithdrawalStatus.Completed;
            const string address = "0x12345678901234567890";

            ClassicAssert.Throws<ArgumentNullException>("id", () => new Withdrawal(null, asset, amount, time, status, address));
            ClassicAssert.Throws<ArgumentNullException>("asset", () => new Withdrawal(id, null, amount, time, status, address));
            ClassicAssert.Throws<ArgumentException>("amount", () => new Withdrawal(id, asset, -1, time, status, address));
            ClassicAssert.Throws<ArgumentException>("amount", () => new Withdrawal(id, asset, 0, time, status, address));
            ClassicAssert.Throws<ArgumentNullException>("address", () => new Withdrawal(id, asset, amount, time, status, null));
            ClassicAssert.Throws<ArgumentNullException>("address", () => new Withdrawal(id, asset, amount, time, status, string.Empty));
        }

        [Fact]
        public void Properties()
        {
            const string id = "1234567890";
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const WithdrawalStatus status = WithdrawalStatus.Completed;
            const string address = "0x12345678901234567890";
            const string addressTag = "ABCDEF";
            const string txId = "21436587092143658709";

            var withdrawal = new Withdrawal(id, asset, amount, time, status, address, addressTag, txId);

            ClassicAssert.Equal(id, withdrawal.Id);
            ClassicAssert.Equal(asset, withdrawal.Asset);
            ClassicAssert.Equal(amount, withdrawal.Amount);
            ClassicAssert.Equal(time, withdrawal.Time);
            ClassicAssert.Equal(status, withdrawal.Status);
            ClassicAssert.Equal(address, withdrawal.Address);
            ClassicAssert.Equal(addressTag, withdrawal.AddressTag);
            ClassicAssert.Equal(txId, withdrawal.TxId);
        }

        [Fact]
        public void Serialization()
        {
            const string id = "1234567890";
            var asset = Asset.BTC;
            const decimal amount = 1.23m;
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            const WithdrawalStatus status = WithdrawalStatus.Completed;
            const string address = "0x12345678901234567890";
            const string addressTag = "ABCDEF";
            const string txId = "21436587092143658709";

            var withdrawal = new Withdrawal(id, asset, amount, time, status, address, addressTag, txId);

            var json = JsonConvert.SerializeObject(withdrawal);

            withdrawal = JsonConvert.DeserializeObject<Withdrawal>(json);

            ClassicAssert.Equal(id, withdrawal.Id);
            ClassicAssert.Equal(asset, withdrawal.Asset);
            ClassicAssert.Equal(amount, withdrawal.Amount);
            ClassicAssert.Equal(time, withdrawal.Time);
            ClassicAssert.Equal(status, withdrawal.Status);
            ClassicAssert.Equal(address, withdrawal.Address);
            ClassicAssert.Equal(addressTag, withdrawal.AddressTag);
            ClassicAssert.Equal(txId, withdrawal.TxId);
        }
    }
}
