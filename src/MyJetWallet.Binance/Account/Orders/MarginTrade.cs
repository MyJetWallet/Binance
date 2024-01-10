namespace MyJetWallet.Binance
{
    public class MarginTrade
    {
        public string symbol { get; set; }
        public long orderId { get; set; }
        public string clientOrderId { get; set; }
        public decimal price { get; set; }
        public decimal origQty { get; set; }
        public decimal executedQty { get; set; }
        public decimal cummulativeQuoteQty { get; set; }
        public string status { get; set; } //"FILLED",
        public string timeInForce { get; set; } //"GTC",
        public string type { get; set; } // "MARKET",
        public string side { get; set; } // "SELL" "BUY",
        public decimal stopPrice { get; set; }
        public decimal icebergQty { get; set; }
        public long time { get; set; }
        public long updateTime { get; set; }
        public bool isWorking { get; set; }
        public long accountId { get; set; }
        public bool isIsolated { get; set; }
    }
}