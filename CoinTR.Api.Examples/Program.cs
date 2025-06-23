using CoinTR.Api.Spot;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CoinTR.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("CoinTR API Console App");
        Console.WriteLine("===================================");
        Console.WriteLine("This is a sample console application to demonstrate the usage of the CoinTR API.");
        Console.WriteLine("Please ensure you have the necessary API keys and permissions to access the CoinTR API.");
        Console.WriteLine("===================================");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);
        Console.WriteLine("Starting...");

        await WebSocketApiExamplesAsync();
        await RestApiExamplesAsync();
    }

    static async Task RestApiExamplesAsync()
    {
        // Rest API Client
        var api = new CoinTRRestApiClient(new CoinTRRestApiClientOptions
        {
            RawResponse = true,
        });
        api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

        var spot_001 = await api.Spot.GetTimeAsync();
        var spot_002 = await api.Spot.GetAssetsAsync();
        var spot_003 = await api.Spot.GetAssetsAsync("USDT");
        var spot_004 = await api.Spot.GetSymbolsAsync();
        var spot_005 = await api.Spot.GetSymbolsAsync("BTCUSDT");
        var spot_006 = await api.Spot.GetTickersAsync();
        var spot_007 = await api.Spot.GetTickersAsync("BTCUSDT");
        var spot_008 = await api.Spot.GetOrderBookAsync("BTCUSDT");
        var spot_009 = await api.Spot.GetOrderBookAsync("BTCUSDT", limit: 20);

        var spot_010 = await api.Spot.PlaceOrderAsync("BTCUSDT", CoinTRSpotOrderSide.Buy, CoinTRSpotOrderType.Limit, CoinTRSpotTimeInForce.GoodTillCanceled, 100.01m, 110000.09m);

        Console.WriteLine("Done!..");
        Console.ReadLine();
    }

    static async Task WebSocketApiExamplesAsync()
    {
        // WebSocket API Client
        var ws = new CoinTRSocketApiClient();
        // ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");
        ws.SetApiCredentials("cointr_e125aa46f672f97dcc76e3c07c4a698e", "9e3ebc3a850fb13d6437bb78add71360f46d31227dd4d595a8d72b14000ed0a4", "bo1144167AZ");

        var sub01 = await ws.Spot.SubscribeToBalancesAsync((data) =>
        {
            Console.WriteLine(JsonConvert.SerializeObject(data.Data));
        });

        Console.WriteLine("Ready!.. "+sub01.Success);
        Console.ReadKey(true);
        var sub02 = await ws.Spot.SubscribeToTickersAsync(["ETHUSDT", "XRPUSDT"], (data) =>
        {
            Console.WriteLine($"{data.Data.Symbol} O:{data.Data.OpenPrice} H:{data.Data.HighPrice} L:{data.Data.LowPrice} C:{data.Data.LastPrice}");
        });

        Console.ReadKey(true);
        await ws.Spot.UnsubscribeAsync(sub02.Data);

        Console.WriteLine("Done!..");
        Console.ReadLine();
    }

}