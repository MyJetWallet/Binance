using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Binance.Utility;
using Xunit;

namespace Binance.Tests.Utility
{
    [Collection("Timing Sensitive Tests")]
    public class WatchdogTimerTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentNullException>("onTimeout", () => new WatchdogTimer(null));
        }

        [Fact]
        public void Properties()
        {
            var interval = TimeSpan.FromMinutes(5);
            const bool isEnabled = true;

            var watchdog = new WatchdogTimer(() => { /* do nothing */ })
            {
                Interval = interval,
                IsEnabled = isEnabled
            };

            ClassicAssert.Equal(interval, watchdog.Interval);
            ClassicAssert.Equal(isEnabled, watchdog.IsEnabled);
        }

        [Fact]
        public async Task Timeout()
        {
            var interval = TimeSpan.FromMilliseconds(500);

            var stopwatch = Stopwatch.StartNew();

            // ReSharper disable once UnusedVariable
            var watchdog = new WatchdogTimer(() => stopwatch.Stop())
            {
                Interval = interval
            };

            watchdog.Kick(); // kick start.

            await Task.Delay(1000);

            ClassicAssert.True(stopwatch.ElapsedMilliseconds <= interval.TotalMilliseconds + 200);
            ClassicAssert.True(stopwatch.ElapsedMilliseconds >= interval.TotalMilliseconds - 200);
        }

        [Fact]
        public async Task Kick()
        {
            var interval = TimeSpan.FromSeconds(1);

            var stopwatch = Stopwatch.StartNew();

            var watchdog = new WatchdogTimer(() => stopwatch.Stop())
            {
                Interval = interval
            };

            const int count = 4;
            const int delay = 500;
            for (var i = 0; i < count; i++)
            {
                watchdog.Kick(); // kick start.

                await Task.Delay(delay);
            }

            ClassicAssert.True(watchdog.IsEnabled);
            // ReSharper disable once ArrangeRedundantParentheses
            ClassicAssert.True(stopwatch.ElapsedMilliseconds <= (count * delay) + 200);
            // ReSharper disable once ArrangeRedundantParentheses
            ClassicAssert.True(stopwatch.ElapsedMilliseconds >= (count * delay) - 200);
        }
    }
}
