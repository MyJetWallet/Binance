using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A candlestick web socket client.
    /// </summary>
    public interface ICandlestickWebSocketClient : IWebSocketPublisherClient, ICandlestickClient
    { }
}
