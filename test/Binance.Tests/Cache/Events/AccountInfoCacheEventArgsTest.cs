﻿using System;
using Binance.Cache;
using Xunit;

namespace Binance.Tests.Cache.Events
{
    public class AccountInfoCacheEventArgsTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentNullException>("accountInfo", () => new AccountInfoCacheEventArgs(null));
        }

        [Fact]
        public void Properties()
        {
            var user = new BinanceApiUser("api-key");
            var commissions = new AccountCommissions(10, 10, 0, 0);
            var status = new AccountStatus(true, true, true);
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var balances = new[] { new AccountBalance("BTC", 0.1m, 0.2m) };

            var accountInfo = new AccountInfo(user, commissions, status, time, balances);

            var args = new AccountInfoCacheEventArgs(accountInfo);

            ClassicAssert.Equal(accountInfo, args.AccountInfo);
        }
    }
}
