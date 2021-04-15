using Newtonsoft.Json;

namespace Binance
{
    public class PlaceOrderConformation
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("clientOrderId")]
        public string ClientOrderId { get; set; }

        [JsonProperty("transactTime")]
        public string TransactTime { get; set; }

        [JsonProperty("marginBuyBorrowAsset")]
        public string MarginBuyBorrowAsset { get; set; }

        [JsonProperty("marginBuyBorrowAmount")]
        public string MarginBuyBorrowAmount { get; set; }

        [JsonProperty("isIsolated")]
        public string IsIsolated { get; set; }


        public static PlaceOrderConformation Parce(string json)
        {
            return JsonConvert.DeserializeObject<PlaceOrderConformation>(json);
        }




    }
}