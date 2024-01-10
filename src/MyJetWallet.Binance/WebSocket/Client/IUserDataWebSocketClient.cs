using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// A user data web socket client.
    /// </summary>
    public interface IUserDataWebSocketClient : IWebSocketPublisherClient, IUserDataClient
    { }
}
