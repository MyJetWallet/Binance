using Newtonsoft.Json;

namespace Binance
{
    public class MarginPair
    {
        [JsonProperty("base")]
        public string Base {get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("isBuyAllowed")]
        public bool IsBuyAllowed { get; set; }

        [JsonProperty("isMarginTrade")]
        public bool IsMarginTrade { get; set; }

        [JsonProperty("isSellAllowed")]
        public bool IsSellAllowed { get; set; }

        [JsonProperty("quote")]
        public string Quote { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}