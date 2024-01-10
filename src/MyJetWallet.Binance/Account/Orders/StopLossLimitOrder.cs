// ReSharper disable once CheckNamespace
namespace MyJetWallet.Binance
{
    public sealed class StopLossLimitOrder : StopLimitOrder
    {
        #region Public Properties

        public override OrderType Type => OrderType.StopLossLimit;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="user">The user (required).</param>
        public StopLossLimitOrder(IBinanceApiUser user)
            : base(user)
        { }

        #endregion Constructors
    }
}
