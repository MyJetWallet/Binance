using MyJetWallet.Binance.Cache;
using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    public interface ITradeWebSocketCache : ITradeWebSocketCache<ITradeWebSocketClient>
    { }

    public interface ITradeWebSocketCache<TClient> : ITradeCache<TClient>
        where TClient : ITradeClient
    { }
}
