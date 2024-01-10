using System;

namespace MyJetWallet.Binance
{
    public interface IChronological
    {
        /// <summary>
        /// Get the time (UTC).
        /// </summary>
        DateTime Time { get; }
    }
}
