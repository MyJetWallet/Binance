using MyJetWallet.Binance.Cache;
using MyJetWallet.Binance.Client;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    public interface ICandlestickWebSocketCache : ICandlestickWebSocketCache<ICandlestickWebSocketClient>
    { }

    public interface ICandlestickWebSocketCache<TClient> : ICandlestickCache<TClient>, IError
        where TClient : ICandlestickClient
    { }
}
