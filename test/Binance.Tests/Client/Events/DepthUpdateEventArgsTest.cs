using System;
using Binance.Client;
using Xunit;

namespace Binance.Tests.Client.Events
{
    public class DepthUpdateEventArgsTest
    {
        [Fact]
        public void Throws()
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var symbol = Symbol.BTC_USDT;
            const long firstUpdateId = 1234567890;
            const long lastUpdateId = 1234567899;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new DepthUpdateEventArgs(time, null, firstUpdateId, lastUpdateId, bids, asks));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new DepthUpdateEventArgs(time, string.Empty, firstUpdateId, lastUpdateId, bids, asks));

            ClassicAssert.Throws<ArgumentException>("firstUpdateId", () => new DepthUpdateEventArgs(time, symbol, -1, lastUpdateId, bids, asks));
            ClassicAssert.Throws<ArgumentException>("lastUpdateId", () => new DepthUpdateEventArgs(time, symbol, firstUpdateId, -1, bids, asks));
            ClassicAssert.Throws<ArgumentException>("lastUpdateId", () => new DepthUpdateEventArgs(time, symbol, firstUpdateId, firstUpdateId - 1, bids, asks));

            ClassicAssert.Throws<ArgumentNullException>("bids", () => new DepthUpdateEventArgs(time, symbol, firstUpdateId, lastUpdateId, null, asks));
            ClassicAssert.Throws<ArgumentNullException>("asks", () => new DepthUpdateEventArgs(time, symbol, firstUpdateId, lastUpdateId, bids, null));
        }

        [Fact]
        public void Properties()
        {
            var time = DateTimeOffset.FromUnixTimeMilliseconds(DateTime.UtcNow.ToTimestamp()).UtcDateTime;
            var symbol = Symbol.BTC_USDT;
            const long firstUpdateId = 1234567890;
            const long lastUpdateId = 1234567899;
            var bids = new(decimal, decimal)[] { (2, 20), (1, 10), (3, 30) };
            var asks = new(decimal, decimal)[] { (6, 60), (4, 40), (5, 50) };

            var args = new DepthUpdateEventArgs(time, symbol, firstUpdateId, lastUpdateId, bids, asks);

            ClassicAssert.Equal(time, args.Time);
            ClassicAssert.Equal(symbol, args.Symbol);

            ClassicAssert.Equal(firstUpdateId, args.FirstUpdateId);
            ClassicAssert.Equal(lastUpdateId, args.LastUpdateId);

            ClassicAssert.NotEmpty(args.Bids);
            ClassicAssert.NotEmpty(args.Asks);
        }
    }
}
