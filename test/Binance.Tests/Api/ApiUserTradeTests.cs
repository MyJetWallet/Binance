using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Binance.Tests.Api;

public class ApiUserTradeTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ApiUserTradeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string ApiKey = "";
    private const string ApiSecret = "";
        
    //[Fact]
    public async Task Balances()
    {
        var user = new BinanceApiUser(ApiKey, ApiSecret);
        var api = new BinanceApi();

        var balance = await api.GetMarginBalancesAsync(user);
        foreach (var b in balance.Where(e => e.Free>0 || e.Borrowed>0))
        {
            _testOutputHelper.WriteLine($"{b.Asset}: {b.Free}|{b.Borrowed}|{b.Locked}|{b.NetAsset}|");
        }

    }
        
    //[Fact]
    public async Task MakeTrade()
    {
        var user = new BinanceApiUser(ApiKey, ApiSecret);
        var api = new BinanceApi();
            
        var request = new MarketOrder(user)
        {
            Symbol = "BUSD_USDT",
            Side = OrderSide.Sell,
            Quantity = 15,
            Id = Guid.NewGuid().ToString()
        };

        var resp = await api.PlaceMarginMarketAsync(request, useBorrow: true);

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resp));


        var orderId = resp.OrderId;

        var resp2 = await api.GetMarginTradesAsync(user, "BUSD_USDT", orderId: orderId);
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resp2));
    }
    
    //[Fact]
    public async Task GetTrades()
    {
        var user = new BinanceApiUser(ApiKey, ApiSecret);
        var api = new BinanceApi();

        var resp2 = await api.GetMarginTradesAsync(user, "ADA_BUSD", limit:10);
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resp2, Formatting.Indented));
    }
    
    //[Fact]
    public async Task MakeTrade_limit()
    {
        var user = new BinanceApiUser(ApiKey, ApiSecret);
        var api = new BinanceApi();
            
        var request = new LimitOrder(user)
        {
            Symbol = "BUSD_USDT",
            Side = OrderSide.Buy,
            Quantity = 1,
            Id = Guid.NewGuid().ToString(),
            Price = 0.95m,
            TimeInForce = TimeInForce.IOC
        };

        var resp = await api.PlaceMarginMarketAsync(request, useBorrow: true);

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(resp));
    }
}