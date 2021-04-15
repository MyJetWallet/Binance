using System;
using System.Linq;
using System.Threading.Tasks;
using Binance;
using Binance.Api;
using Binance.Cache;
using Binance.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RestAPI();

            WebSocket();
        }

        private static void WebSocket()
        {
            var webSocketCacheXlm = new DepthWebSocketCache();
            var webSocketCacheEth = new DepthWebSocketCache();

            // Handle error events.
            webSocketCacheXlm.Error += (s, e) => { Console.WriteLine(e.Exception.Message); };
            webSocketCacheEth.Error += (s, e) => { Console.WriteLine(e.Exception.Message); };

            // Subscribe callback to BTC/USDT (automatically begin streaming).
            //webSocketCacheXlm.Subscribe(Symbol.XLM_USDT, ReceiveOrderBook);
            //webSocketCacheEth.Subscribe(Symbol.ETH_BTC, ReceiveOrderBook);

            webSocketCacheXlm.Subscribe(Symbol.XLM_USDT);
            webSocketCacheEth.Subscribe(Symbol.ETH_BTC);

            var cmd = Console.ReadLine();
            while (cmd != "exit")
            {
                if (cmd == "xlm")
                {
                    var orderBook = webSocketCacheXlm.OrderBook;
                    PrintOrderBook(orderBook);
                }

                if (cmd == "eth")
                {
                    var orderBook = webSocketCacheEth.OrderBook;
                    PrintOrderBook(orderBook);
                }

                cmd = Console.ReadLine();
            }

            // Unsubscribe (automatically end streaming).
            webSocketCacheXlm.Unsubscribe();
            webSocketCacheEth.Unsubscribe();
        }

        private static void ReceiveOrderBook(OrderBookCacheEventArgs evt)
        {
            var orderBook = evt.OrderBook;

            PrintOrderBook(orderBook);
        }

        private static void PrintOrderBook(OrderBook orderBook)
        {
            var symbol = Symbol.Cache.Get(orderBook.Symbol);

            var minBidPrice = orderBook.Bids.Last().Price;
            var maxAskPrice = orderBook.Asks.Last().Price;

            // Handle order book update events.
            Console.Write($"Bid Quantity: {orderBook.Depth(minBidPrice)} {symbol.BaseAsset} - " +
                          $"Ask Quantity: {orderBook.Depth(maxAskPrice)} {symbol.BaseAsset}");

            var bid = orderBook.Bids.First().Price;
            var ask = orderBook.Asks.First().Price;

            Console.WriteLine($"    bid: {bid}[{orderBook.Bids.Count()}]  ask: {ask}[{orderBook.Asks.Count()}]");
        }

        private static async Task RestAPI()
        {
            var api = new BinanceApi();

            if (await api.PingAsync())
            {
                Console.WriteLine("Successful!");
            }
            else
            {
                Console.WriteLine("Fail!");
            }


            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("API_SECRET");

            using var user = new BinanceApiUser(apiKey, apiSecret);


            await Symbol.UpdateCacheAsync(api);


            Console.WriteLine(Symbol.Cache.GetAll().Count());
            Console.WriteLine(Symbol.Cache.Get("BTCUSD"));
            Console.WriteLine(Symbol.Cache.Get("BTCEUR"));
            Console.WriteLine($"{Symbol.Cache.Get("BTCUSDT")}  {Symbol.Cache.Get("BTCUSDT")?.IsMarginTradingAllowed}");


            try
            {
                //var clientOrder = new MarketOrder(user)
                //{
                //    Symbol = Symbol.XLM_USDT,
                //    Side = OrderSide.Sell,
                //    Quantity = 100m
                //};
                //var conf = await api.PlaceMarginMarketAsync(clientOrder, true);
                //Console.WriteLine(JsonConvert.SerializeObject(conf));


                var xlmMaxBorrow = api.GetMaxBorrowAsync(user, "XLM");
                var usdMaxBorrow = api.GetMaxBorrowAsync(user, "USDT");
                var btcMaxBorrow = api.GetMaxBorrowAsync(user, "BTC");
                await Task.WhenAll(new[] {xlmMaxBorrow, usdMaxBorrow, btcMaxBorrow});

                Console.WriteLine($"Xlm: {xlmMaxBorrow.Result}");
                Console.WriteLine($"usd: {usdMaxBorrow.Result}");
                Console.WriteLine($"usd: {btcMaxBorrow.Result}");


                var pairs = await api.GetMarginPairsAsync(user);
                Console.WriteLine(JsonConvert.SerializeObject(pairs, Formatting.Indented));
            }
            catch (BinanceRequestRateLimitExceededException ex)
            {
                Console.WriteLine(
                    $"TEST Order HTTP Failed: Status: {ex.StatusCode}, Code: {ex.ErrorCode}, Message: {ex.ErrorMessage}");
            }
            catch (BinanceHttpException ex)
            {
                Console.WriteLine(
                    $"TEST Order HTTP Failed: Status: {ex.StatusCode}, Code: {ex.ErrorCode}, Message: {ex.ErrorMessage}");
            }
            catch (BinanceApiException e)
            {
                Console.WriteLine($"TEST Order Failed: \"{e.Message}\"");
            }

            catch (Exception e)
            {
                Console.WriteLine($"TEST Order Exception: \"{e.Message}\"");
            }
        }
    }
}
