namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A MyJetWallet.Binance web socket JSON stream.
    /// </summary>
    public interface IBinanceWebSocketStream : IWebSocketStream
    {
        /// <summary>
        /// Get the flag indicating if using combined streams.
        /// </summary>
        bool IsCombined { get; }
    }
}
