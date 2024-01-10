using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// An aggregate trade web socket client.
    /// </summary>
    public interface IAggregateTradeWebSocketClient : IWebSocketPublisherClient, IAggregateTradeClient
    { }
}
