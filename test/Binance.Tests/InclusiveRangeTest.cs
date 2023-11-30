using System;
using Xunit;

namespace Binance.Tests
{
    public class InclusiveRangeTest
    {
        [Fact]
        public void Throws()
        {
            ClassicAssert.Throws<ArgumentException>("minimum", () => new InclusiveRange(-1, 1, 1));
            ClassicAssert.Throws<ArgumentException>("maximum", () => new InclusiveRange(1, -1, 1));
            ClassicAssert.Throws<ArgumentException>("increment", () => new InclusiveRange(1, 1, -1));
            ClassicAssert.Throws<ArgumentException>("increment", () => new InclusiveRange(1, 1, 0));
        }

        [Fact]
        public void ImplicitOperators()
        {
            const decimal minimum = 0.001m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = (InclusiveRange)(minimum, maximum, increment);

            ClassicAssert.Equal(minimum, range.Minimum);
            ClassicAssert.Equal(maximum, range.Maximum);
            ClassicAssert.Equal(increment, range.Increment);
        }

        [Fact]
        public void Properties()
        {
            const decimal minimum = 0.001m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            ClassicAssert.Equal(minimum, range.Minimum);
            ClassicAssert.Equal(maximum, range.Maximum);
            ClassicAssert.Equal(increment, range.Increment);
        }

        [Fact]
        public void IsValid()
        {
            const decimal minimum = 0.01m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            ClassicAssert.True(range.IsValid(minimum));
            ClassicAssert.True(range.IsValid(maximum));
            ClassicAssert.True(range.IsValid(minimum + increment));
            ClassicAssert.True(range.IsValid(maximum - increment));

            ClassicAssert.False(range.IsValid(minimum - increment));
            ClassicAssert.False(range.IsValid(maximum + increment));
            ClassicAssert.False(range.IsValid(minimum + increment / 2));
            ClassicAssert.False(range.IsValid(maximum - increment / 2));
        }

        [Fact]
        public void Validate()
        {
            const decimal minimum = 0.01m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            range.Validate(minimum);
            range.Validate(maximum);
            range.Validate(minimum + increment);
            range.Validate(maximum - increment);

            ClassicAssert.Throws<ArgumentOutOfRangeException>("value", () => range.Validate(minimum - increment));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("value", () => range.Validate(maximum + increment));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("value", () => range.Validate(minimum + increment / 2));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("value", () => range.Validate(maximum - increment / 2));
        }

        [Fact]
        public void GetUpperValidValue()
        {
            const decimal minimum = 0.01m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            ClassicAssert.Equal(maximum, range.GetUpperValidValue(maximum));
            ClassicAssert.Equal(minimum, range.GetUpperValidValue(minimum));
            ClassicAssert.Equal(minimum + increment, range.GetUpperValidValue(minimum + increment));
            ClassicAssert.Equal(maximum - increment, range.GetUpperValidValue(maximum - increment));

            ClassicAssert.Equal(1.11m, range.GetUpperValidValue(1.110m));
            ClassicAssert.Equal(1.12m, range.GetUpperValidValue(1.112m));
            ClassicAssert.Equal(1.12m, range.GetUpperValidValue(1.115m));
            ClassicAssert.Equal(1.12m, range.GetUpperValidValue(1.118m));
            ClassicAssert.Equal(1.12m, range.GetUpperValidValue(1.120m));
            ClassicAssert.Equal(1.13m, range.GetUpperValidValue(1.122m));
            ClassicAssert.Equal(1.13m, range.GetUpperValidValue(1.125m));
            ClassicAssert.Equal(1.13m, range.GetUpperValidValue(1.128m));

            // 1.234 => 1.240 given range of [0.01 - 10.00] with increment of 0.01.
            ClassicAssert.Equal(1.240m, range.GetUpperValidValue(1.234m));
        }

        [Fact]
        public void GetLowerValidValue()
        {
            const decimal minimum = 0.01m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            ClassicAssert.Equal(maximum, range.GetLowerValidValue(maximum));
            ClassicAssert.Equal(minimum, range.GetLowerValidValue(minimum));
            ClassicAssert.Equal(minimum + increment, range.GetLowerValidValue(minimum + increment));
            ClassicAssert.Equal(maximum - increment, range.GetLowerValidValue(maximum - increment));

            ClassicAssert.Equal(1.11m, range.GetLowerValidValue(1.110m));
            ClassicAssert.Equal(1.11m, range.GetLowerValidValue(1.112m));
            ClassicAssert.Equal(1.11m, range.GetLowerValidValue(1.115m));
            ClassicAssert.Equal(1.11m, range.GetLowerValidValue(1.118m));
            ClassicAssert.Equal(1.12m, range.GetLowerValidValue(1.120m));
            ClassicAssert.Equal(1.12m, range.GetLowerValidValue(1.122m));
            ClassicAssert.Equal(1.12m, range.GetLowerValidValue(1.125m));
            ClassicAssert.Equal(1.12m, range.GetLowerValidValue(1.128m));

            // 9.876 => 9.870 given range of [0.01 - 10.00] with increment of 0.01.
            ClassicAssert.Equal(9.870m, range.GetLowerValidValue(9.876m));
        }

        [Fact]
        public void GetValidValue()
        {
            const decimal minimum = 0.01m;
            const decimal maximum = 10.0m;
            const decimal increment = 0.01m;

            var range = new InclusiveRange(minimum, maximum, increment);

            ClassicAssert.Equal(maximum, range.GetValidValue(maximum));
            ClassicAssert.Equal(minimum, range.GetValidValue(minimum));
            ClassicAssert.Equal(minimum + increment, range.GetValidValue(minimum + increment));
            ClassicAssert.Equal(maximum - increment, range.GetValidValue(maximum - increment));

            var midpointRounding = MidpointRounding.ToEven;

            ClassicAssert.Equal(1.11m, range.GetValidValue(1.110m, midpointRounding));
            ClassicAssert.Equal(1.11m, range.GetValidValue(1.112m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.115m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.118m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.120m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.122m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.125m, midpointRounding));
            ClassicAssert.Equal(1.13m, range.GetValidValue(1.128m, midpointRounding));

            midpointRounding = MidpointRounding.AwayFromZero;

            ClassicAssert.Equal(1.11m, range.GetValidValue(1.110m, midpointRounding));
            ClassicAssert.Equal(1.11m, range.GetValidValue(1.112m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.115m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.118m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.120m, midpointRounding));
            ClassicAssert.Equal(1.12m, range.GetValidValue(1.122m, midpointRounding));
            ClassicAssert.Equal(1.13m, range.GetValidValue(1.125m, midpointRounding));
            ClassicAssert.Equal(1.13m, range.GetValidValue(1.128m, midpointRounding));

            // 1.234 => 1.230 given range of [0.01 - 10.00] with increment of 0.01.
            ClassicAssert.Equal(1.230m, range.GetValidValue(1.234m));
            // 2.345 => 2.340 given range of [0.01 - 10.00] with increment of 0.01 (midpoint rounding to even).
            // ReSharper disable once RedundantArgumentDefaultValue
            ClassicAssert.Equal(2.340m, range.GetValidValue(2.345m, MidpointRounding.ToEven));
            // 2.345 => 2.350 given range of [0.01 - 10.00] with increment of 0.01 (midpoint rounding away from 0).
            ClassicAssert.Equal(2.350m, range.GetValidValue(2.345m, MidpointRounding.AwayFromZero));
            // 9.876 => 9.880 given range of [0.01 - 10.00] with increment of 0.01.
            ClassicAssert.Equal(9.880m, range.GetValidValue(9.876m));
        }
    }
}
