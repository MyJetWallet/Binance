// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace

using System;

namespace MyJetWallet.Binance
{
    public enum SideEffect
    {
        //NO_SIDE_EFFECT, MARGIN_BUY, AUTO_REPAY; default NO_SIDE_EFFECT.
        NoSideEffect,

        MarginBuy,

        AutoRepay
    }

    public static class SideEffectExtensions
    {
        public static string ToRequestString(this SideEffect? sideEffect)
        {
            switch (sideEffect)
            {
                case SideEffect.NoSideEffect:
                    return "NO_SIDE_EFFECT";
                case SideEffect.MarginBuy:
                    return "MARGIN_BUY";
                case SideEffect.AutoRepay:
                    return "AUTO_REPAY";
                default:
                    return String.Empty;
            }
        }
    }
    
    
}
