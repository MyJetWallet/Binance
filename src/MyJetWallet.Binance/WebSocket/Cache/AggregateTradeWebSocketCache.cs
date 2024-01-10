using System;
using Microsoft.Extensions.Logging;
using MyJetWallet.Binance.Cache;

// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance.WebSocket
{
    /// <summary>
    /// The default <see cref="IAggregateTradeWebSocketCache"/> implementation.
    /// </summary>
    public class AggregateTradeWebSocketCache : AggregateTradeCache<IAggregateTradeWebSocketClient>, IAggregateTradeWebSocketCache
    {
        #region Public Events

        public event EventHandler<ErrorEventArgs> Error
        {
            add => Client.Error += value;
            remove => Client.Error -= value;
        }

        #endregion Public Events

        #region Constructors

        /// <summary>
        /// Default constructor provides default <see cref="IBinanceApi"/>
        /// and default <see cref="IAggregateTradeWebSocketClient"/>, but no logger.
        /// </summary>
        public AggregateTradeWebSocketCache()
            : this(new BinanceApi(), new AggregateTradeWebSocketClient())
        { }

        /// <summary>
        /// The DI constructor.
        /// </summary>
        /// <param name="api">The MyJetWallet.Binance API (required).</param>
        /// <param name="client">The web socket client (required).</param>
        /// <param name="logger">The logger (optional).</param>
        public AggregateTradeWebSocketCache(IBinanceApi api, IAggregateTradeWebSocketClient client, ILogger<AggregateTradeWebSocketCache> logger = null)
            : base(api, client, logger)
        { }

        #endregion Construtors
    }
}
