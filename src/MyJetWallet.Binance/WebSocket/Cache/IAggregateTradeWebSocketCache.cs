using MyJetWallet.Binance.Cache;
using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    public interface IAggregateTradeWebSocketCache : IAggregateTradeWebSocketCache<IAggregateTradeWebSocketClient>
    { }

    public interface IAggregateTradeWebSocketCache<TClient> : IAggregateTradeCache<TClient>, IError
        where TClient : IAggregateTradeClient
    { }
}
