using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A depth web socket client.
    /// </summary>
    public interface IDepthWebSocketClient : IWebSocketPublisherClient, IDepthClient
    { }
}
