using MyJetWallet.Binance.Stream;

namespace MyJetWallet.Binance.WebSocket
{
    public interface IBinanceWebSocketStreamPublisher : IAutoJsonStreamPublisher<IBinanceWebSocketStream>
    { }
}
