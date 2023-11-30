﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using Binance.Api;

namespace Binance.Tests.Api
{
    [Collection("Timing Sensitive Tests")]
    public class RateLimiterTest
    {
        [Fact]
        public void CountThrows()
        {
            var rateLimiter = new RateLimiter();

            ClassicAssert.Throws<ArgumentException>(nameof(rateLimiter.Count), () => rateLimiter.Count = -1);
            ClassicAssert.Throws<ArgumentException>(nameof(rateLimiter.Count), () => rateLimiter.Count = 0);
        }

        [Fact]
        public void Properties()
        {
            const int count = 3;
            var duration = TimeSpan.FromSeconds(3);

            var rateLimiter = new RateLimiter
            {
                Count = count,
                Duration = duration
            };

            ClassicAssert.Equal(count, rateLimiter.Count);
            ClassicAssert.Equal(duration, rateLimiter.Duration);
        }

        [Fact]
        public async Task RateLimit()
        {
            const int count = 100;
            var duration = TimeSpan.FromSeconds(1);

            const int repetitions = 5;

            var rateLimiter = new RateLimiter
            {
                Count = count,
                Duration = duration
            };

            var stopwatch = new Stopwatch();

            for (var i = 0; i < repetitions; i++)
            {
                stopwatch.Restart();

                for (var j = 0; j < count + 1; j++)
                    await rateLimiter.DelayAsync();

                stopwatch.Stop();

                // Assume elapsed milliseconds is within +/- 60 msec of expected time (15 msec clock resolution).
                // NOTE: Should account for errors in two timestamps, Task.Delay(), and Stopwatch.
                ClassicAssert.True(stopwatch.ElapsedMilliseconds >= duration.TotalMilliseconds - 60);
                ClassicAssert.False(stopwatch.ElapsedMilliseconds > duration.TotalMilliseconds + 60);
            }
        }
    }
}
