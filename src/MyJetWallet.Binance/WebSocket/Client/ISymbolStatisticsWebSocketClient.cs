using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A symbol [24-hour] statistics web socket client.
    /// </summary>
    public interface ISymbolStatisticsWebSocketClient : IWebSocketPublisherClient, ISymbolStatisticsClient
    { }
}
