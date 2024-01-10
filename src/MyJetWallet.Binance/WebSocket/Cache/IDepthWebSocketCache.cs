using MyJetWallet.Binance.Cache;
using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    public interface IDepthWebSocketCache : IDepthWebSocketCache<IDepthWebSocketClient>
    { }

    public interface IDepthWebSocketCache<TClient> : IOrderBookCache<TClient>, IError
        where TClient : IDepthClient
    { }
}
