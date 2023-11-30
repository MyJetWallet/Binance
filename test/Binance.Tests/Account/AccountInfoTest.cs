using System;
using System.Linq;
using Xunit;

namespace Binance.Tests.Account
{
    public class AccountInfoTest
    {
        [Fact]
        public void Throws()
        {
            var user = new BinanceApiUser("api-key");
            var commissions = new AccountCommissions(10, 10, 0, 0);
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var status = new AccountStatus(true, true, true);

            ClassicAssert.Throws<ArgumentNullException>("user", () => new AccountInfo(null, commissions, status, time));
            ClassicAssert.Throws<ArgumentNullException>("commissions", () => new AccountInfo(user, null, status, time));
            ClassicAssert.Throws<ArgumentNullException>("status", () => new AccountInfo(user, commissions, null, time));
        }

        [Fact]
        public void Properties()
        {
            var user = new BinanceApiUser("api-key");
            var commissions = new AccountCommissions(10, 10, 0, 0);
            var status = new AccountStatus(true, true, true);
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var balances = new[] { new AccountBalance("BTC", 0.1m, 0.2m) };

            var account = new AccountInfo(user, commissions, status, time);

            ClassicAssert.Equal(commissions, account.Commissions);
            ClassicAssert.Equal(status, account.Status);
            ClassicAssert.Equal(time, account.Time);
            ClassicAssert.NotNull(account.Balances);
            ClassicAssert.Empty(account.Balances);

            account = new AccountInfo(user, commissions, status, time, balances);

            ClassicAssert.Equal(commissions, account.Commissions);
            ClassicAssert.Equal(status, account.Status);
            ClassicAssert.Equal(time, account.Time);
            ClassicAssert.NotNull(account.Balances);
            ClassicAssert.NotEmpty(account.Balances);
            ClassicAssert.Equal(balances[0].Asset, account.Balances.First().Asset);
        }
    }
}
