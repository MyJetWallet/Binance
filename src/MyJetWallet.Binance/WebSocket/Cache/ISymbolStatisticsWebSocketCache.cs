using MyJetWallet.Binance.Cache;
using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    public interface ISymbolStatisticsWebSocketCache : ISymbolStatisticsWebSocketCache<ISymbolStatisticsWebSocketClient>
    { }

    public interface ISymbolStatisticsWebSocketCache<TClient> : ISymbolStatisticsCache<TClient>, IError
        where TClient : ISymbolStatisticsClient
    { }
}
