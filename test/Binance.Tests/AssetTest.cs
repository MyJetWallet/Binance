using System;
using Xunit;

namespace Binance.Tests
{
    public class AssetTest
    {
        [Fact]
        public void Throws()
        {
            const int precision = 8;

            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new Asset(null, precision));
            ClassicAssert.Throws<ArgumentNullException>("symbol", () => new Asset(string.Empty, precision));
        }

        [Fact]
        public void ImplicitOperators()
        {
            var btc1 = Asset.BTC;
            var btc2 = new Asset(btc1.Symbol, btc1.Precision);
            var xrp = Asset.XRP;

            ClassicAssert.True(btc1 == btc2);
            ClassicAssert.True(btc1 != xrp);

            ClassicAssert.True(btc1 == btc1.Symbol);
            ClassicAssert.True(btc1 != xrp.Symbol);
        }

        [Fact]
        public void Properties()
        {
            const string symbol = "BTC";
            const int precision = 8;

            var btc = new Asset(symbol, precision);

            ClassicAssert.Equal(symbol, btc.Symbol);
            ClassicAssert.Equal(precision, btc.Precision);
        }

        [Fact]
        public void IsValid()
        {
            var validAsset = Asset.BTC;
            var invalidAsset = new Asset("...", 0);

            ClassicAssert.True(Asset.IsValid(validAsset));
            ClassicAssert.False(Asset.IsValid(invalidAsset));
        }

        [Fact]
        public void IsAmountValid()
        {
            var asset = Asset.BTC;

            ClassicAssert.Equal(8, asset.Precision);

            ClassicAssert.True(asset.IsAmountValid(100.000000000000m));
            ClassicAssert.True(asset.IsAmountValid(010.000000000000m));
            ClassicAssert.True(asset.IsAmountValid(001.000000000000m));
            ClassicAssert.True(asset.IsAmountValid(000.100000000000m));
            ClassicAssert.True(asset.IsAmountValid(000.010000000000m));
            ClassicAssert.True(asset.IsAmountValid(000.001000000000m));
            ClassicAssert.True(asset.IsAmountValid(000.000100000000m));
            ClassicAssert.True(asset.IsAmountValid(000.000010000000m));
            ClassicAssert.True(asset.IsAmountValid(000.000001000000m));
            ClassicAssert.True(asset.IsAmountValid(000.000000100000m));
            ClassicAssert.True(asset.IsAmountValid(000.000000010000m));

            ClassicAssert.False(asset.IsAmountValid(000.000000001000m));
            ClassicAssert.False(asset.IsAmountValid(000.000000000100m));
            ClassicAssert.False(asset.IsAmountValid(000.000000000010m));
            ClassicAssert.False(asset.IsAmountValid(000.000000000001m));

            ClassicAssert.True(asset.IsAmountValid(000.000000000000m));

            ClassicAssert.False(asset.IsAmountValid(-000.000000010000m));
        }

        [Fact]
        public void ValidateAmount()
        {
            var asset = Asset.BTC;

            ClassicAssert.Equal(8, asset.Precision);

            asset.ValidateAmount(100.000000000000m);
            asset.ValidateAmount(010.000000000000m);
            asset.ValidateAmount(001.000000000000m);
            asset.ValidateAmount(000.100000000000m);
            asset.ValidateAmount(000.010000000000m);
            asset.ValidateAmount(000.001000000000m);
            asset.ValidateAmount(000.000100000000m);
            asset.ValidateAmount(000.000010000000m);
            asset.ValidateAmount(000.000001000000m);
            asset.ValidateAmount(000.000000100000m);
            asset.ValidateAmount(000.000000010000m);

            ClassicAssert.Throws<ArgumentOutOfRangeException>("amount", () => asset.ValidateAmount(000.000000001000m));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("amount", () => asset.ValidateAmount(000.000000000100m));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("amount", () => asset.ValidateAmount(000.000000000010m));
            ClassicAssert.Throws<ArgumentOutOfRangeException>("amount", () => asset.ValidateAmount(000.000000000001m));

            asset.ValidateAmount(000.000000000000m);

            ClassicAssert.Throws<ArgumentOutOfRangeException>("amount", () => asset.ValidateAmount(-000.000000010000m));
        }
    }
}
