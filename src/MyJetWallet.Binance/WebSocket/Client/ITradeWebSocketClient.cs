using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A trade web socket client.
    /// </summary>
    public interface ITradeWebSocketClient : IWebSocketPublisherClient, ITradeClient
    { }
}
