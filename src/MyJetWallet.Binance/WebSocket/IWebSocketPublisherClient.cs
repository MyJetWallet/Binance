using MyJetWallet.Binance.Client;
using MyJetWallet.Binance.Stream;

namespace MyJetWallet.Binance.WebSocket
{
    public interface IWebSocketPublisherClient : IJsonPublisherClient<IAutoJsonStreamPublisher<IWebSocketStream>>, IError
    { }
}
