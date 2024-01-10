using System;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance
{
    /// <summary>
    /// MyJetWallet.Binance API exception.
    /// </summary>
    public class BinanceApiException : Exception
    {
        #region Constructors

        public BinanceApiException()
        { }

        public BinanceApiException(string message)
            : base(message)
        { }

        public BinanceApiException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}
