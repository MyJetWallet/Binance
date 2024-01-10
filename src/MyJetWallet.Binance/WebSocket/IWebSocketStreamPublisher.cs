using MyJetWallet.Binance.Stream;

namespace MyJetWallet.Binance.WebSocket
{
    public interface IWebSocketStreamPublisher : IAutoJsonStreamPublisher<IWebSocketStream>
    { }
}
