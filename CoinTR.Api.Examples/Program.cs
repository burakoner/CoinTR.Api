using CoinTR.Api.Shared;
using CoinTR.Api.Spot;
using System;
using System.Threading.Tasks;

namespace CoinTR.Api.Examples;

internal class Program
{
    static async Task Main(string[] args)
    {
        // WebSocket API Client
        var ws = new CoinTRSocketApiClient();
        //ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        var spot = (CoinTRSpotSocketClient)ws.Spot;

        var sub02 = await spot.SubscribeToTickersAsync(["ETHUSDT", "XRPUSDT"], (data) =>
        {
            Console.WriteLine($"{data.Data.Symbol} O:{data.Data.OpenPrice} H:{data.Data.HighPrice} L:{data.Data.LowPrice} C:{data.Data.LastPrice}");
        });
        Console.ReadKey(true);
        await ws.Spot.UnsubscribeAsync(sub02.Data);

        Console.WriteLine("CoinTR API Console App");
        Console.WriteLine("===================================");
        Console.WriteLine("This is a sample console application to demonstrate the usage of the CoinTR API.");
        Console.WriteLine("Please ensure you have the necessary API keys and permissions to access the CoinTR API.");
        Console.WriteLine("===================================");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);
        Console.WriteLine("Starting...");

        await RestApiExamplesAsync();
        await WebSocketApiExamplesAsync();
    }

    static async Task RestApiExamplesAsync()
    {
        // Rest API Client
        var api = new CoinTRRestApiClient(new CoinTRRestApiClientOptions
        {
            RawResponse = true,
        });
        api.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

        var spot = (CoinTRSpotRestClient)api.Spot;

        /*
        var spot_001 = await spot.GetTimeAsync();
        var spot_002 = await spot.GetAssetsAsync();
        var spot_003 = await spot.GetAssetsAsync("USDT");
        var spot_004 = await spot.GetSymbolsAsync();
        var spot_005 = await spot.GetSymbolsAsync("BTCUSDT");
        var spot_006 = await spot.GetTickersAsync();
        var spot_007 = await spot.GetTickersAsync("BTCUSDT");
        */
        var spot_008 = await spot.GetOrderBookAsync("BTCUSDT");
        var spot_009 = await spot.GetOrderBookAsync("BTCUSDT", limit: 20);

        var order = await spot.PlaceOrderAsync("BTCUSDT", CoinTRSpotOrderSide.Buy, CoinTRSpotOrderType.Limit, CoinTRSpotTimeInForce.GoodTillCanceled, 100.01m, 110000.09m);


        var a = 0;




        /*
        var spot_201 = await api.Spot.GetOrderBookAsync("BTCUSDT");
        var spot_202 = await api.Spot.GetRecentTradesAsync("BTCUSDT");
        var spot_203 = await api.Spot.GetHistoricalTradesAsync("BTCUSDT");
        var spot_204 = await api.Spot.GetAggregatedTradesAsync("BTCUSDT");
        var spot_205 = await api.Spot.GetKlinesAsync("BTCUSDT", BinanceKlineInterval.OneDay);
        var spot_206 = await api.Spot.GetUIKlinesAsync("BTCUSDT", BinanceKlineInterval.OneDay);
        var spot_207 = await api.Spot.GetAveragePriceAsync("BTCUSDT");
        var spot_211 = await api.Spot.GetTickerAsync("BTCUSDT");
        var spot_212 = await api.Spot.GetTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_213 = await api.Spot.GetTickersAsync();
        var spot_214 = await api.Spot.GetMiniTickerAsync("BTCUSDT");
        var spot_215 = await api.Spot.GetMiniTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_216 = await api.Spot.GetMiniTickersAsync();
        var spot_221 = await api.Spot.GetTradingDayTickerAsync("BTCUSDT");
        var spot_222 = await api.Spot.GetTradingDayTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_223 = await api.Spot.GetTradingDayTickersAsync();
        var spot_224 = await api.Spot.GetTradingDayMiniTickerAsync("BTCUSDT");
        var spot_225 = await api.Spot.GetTradingDayMiniTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_226 = await api.Spot.GetTradingDayMiniTickersAsync();
        var spot_231 = await api.Spot.GetPriceTickerAsync("BTCUSDT");
        var spot_232 = await api.Spot.GetPriceTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_233 = await api.Spot.GetPriceTickersAsync();
        var spot_241 = await api.Spot.GetBookTickerAsync("BTCUSDT");
        var spot_242 = await api.Spot.GetBookTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_243 = await api.Spot.GetBookTickersAsync();
        var spot_251 = await api.Spot.GetRollingWindowTickerAsync("BTCUSDT");
        var spot_252 = await api.Spot.GetRollingWindowTickersAsync(["BTCUSDT", "ETHUSDT"], TimeSpan.FromHours(4));

        // Spot > Trading Methods (PRIVATE)
        var spot_301 = await api.Spot.PlaceOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, 0.01m);
        var spot_302 = await api.Spot.PlaceTestOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, 0.01m);
        var spot_303 = await api.Spot.GetOrderAsync("BTCUSDT", orderId: 100000001);
        var spot_304 = await api.Spot.GetOrderAsync("BTCUSDT", origClientOrderId: "---CLIENT-ORDER-ID---");
        var spot_305 = await api.Spot.CancelOrderAsync("BTCUSDT", orderId: 100000001);
        var spot_306 = await api.Spot.CancelOrderAsync("BTCUSDT", origClientOrderId: "---CLIENT-ORDER-ID---");
        var spot_307 = await api.Spot.CancelOrdersAsync("BTCUSDT");
        var spot_308 = await api.Spot.ReplaceOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, BinanceSpotOrderCancelReplaceMode.StopOnFailure, cancelOrderId: 100000001, quantity: 0.1m);
        var spot_309 = await api.Spot.GetOpenOrdersAsync("BTCUSDT");
        var spot_310 = await api.Spot.GetOrdersAsync("BTCUSDT");

        // Spot > Account Methods (PRIVATE)
        var spot_401 = await api.Spot.GetAccountAsync();
        var spot_402 = await api.Spot.GetUserTradesAsync("BTCUSDT");
        var spot_403 = await api.Spot.GetRateLimitsAsync();
        var spot_404 = await api.Spot.GetPreventedTradesAsync("BTCUSDT", orderId: 100000001);
        var spot_405 = await api.Spot.StartUserStreamAsync();
        var spot_406 = await api.Spot.KeepAliveUserStreamAsync("---LISTEN-KEY---");
        var spot_407 = await api.Spot.StopUserStreamAsync("---LISTEN-KEY---");

        // TODO: European Options -> User Data Stream Methods (PRIVATE)
        var options_501 = await api.Spot.StartUserStreamAsync();
        var options_502 = await api.Spot.KeepAliveUserStreamAsync("---LISTEN-KEY---");
        var options_503 = await api.Spot.StopUserStreamAsync("---LISTEN-KEY---");
    }

    static async Task WebSocketApiQueryExamplesAsync()
    {
        // WebSocket API Client
        var ws = new CoinTRSocketApiClient();
        ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");

        // Spot Web Socket API > General Methods (PUBLIC)
        var spot_101 = await ws.Spot.PingAsync();
        var spot_102 = await ws.Spot.GetTimeAsync();
        var spot_103 = await ws.Spot.GetExchangeInfoAsync();

        // Spot Web Socket API > Market Data Query Methods (PUBLIC)
        var spot_201 = await ws.Spot.GetOrderBookAsync("BTCUSDT");
        var spot_202 = await ws.Spot.GetRecentTradesAsync("BTCUSDT");
        var spot_203 = await ws.Spot.GetHistoricalTradesAsync("BTCUSDT");
        var spot_204 = await ws.Spot.GetAggregatedTradesAsync("BTCUSDT");
        var spot_205 = await ws.Spot.GetKlinesAsync("BTCUSDT", BinanceKlineInterval.OneDay);
        var spot_206 = await ws.Spot.GetUIKlinesAsync("BTCUSDT", BinanceKlineInterval.OneDay);
        var spot_207 = await ws.Spot.GetAveragePriceAsync("BTCUSDT");
        var spot_211 = await ws.Spot.GetTickerAsync("BTCUSDT");
        var spot_212 = await ws.Spot.GetTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_214 = await ws.Spot.GetTickersAsync();
        var spot_215 = await ws.Spot.GetMiniTickerAsync("BTCUSDT");
        var spot_216 = await ws.Spot.GetMiniTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_217 = await ws.Spot.GetMiniTickersAsync();
        var spot_221 = await ws.Spot.GetTradingDayTickerAsync("BTCUSDT");
        var spot_222 = await ws.Spot.GetTradingDayTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_223 = await ws.Spot.GetTradingDayTickersAsync();
        var spot_224 = await ws.Spot.GetTradingDayMiniTickerAsync("BTCUSDT");
        var spot_225 = await ws.Spot.GetTradingDayMiniTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_226 = await ws.Spot.GetTradingDayMiniTickersAsync();
        var spot_231 = await ws.Spot.GetRollingWindowTickerAsync("BTCUSDT");
        var spot_232 = await ws.Spot.GetRollingWindowTickersAsync(["BTCUSDT", "ETHUSDT"], TimeSpan.FromHours(4));
        var spot_241 = await ws.Spot.GetBookTickerAsync("BTCUSDT");
        var spot_242 = await ws.Spot.GetBookTickersAsync(["BTCUSDT", "ETHUSDT"]);
        var spot_243 = await ws.Spot.GetBookTickersAsync();

        // Spot Web Socket API > Trading Query Methods (PRIVATE)
        var spot_301 = await ws.Spot.PlaceOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, 0.01m);
        var spot_302 = await ws.Spot.PlaceTestOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, 0.01m);
        var spot_303 = await ws.Spot.GetOrderAsync("BTCUSDT", orderId: 100000001);
        var spot_304 = await ws.Spot.GetOrderAsync("BTCUSDT", origClientOrderId: "---CLIENT-ORDER-ID---");
        var spot_305 = await ws.Spot.CancelOrderAsync("BTCUSDT", orderId: 100000001);
        var spot_306 = await ws.Spot.CancelOrderAsync("BTCUSDT", origClientOrderId: "---CLIENT-ORDER-ID---");
        var spot_307 = await ws.Spot.ReplaceOrderAsync("BTCUSDT", BinanceOrderSide.Buy, BinanceSpotOrderType.Market, BinanceSpotOrderCancelReplaceMode.StopOnFailure, cancelOrderId: 100000001, quantity: 0.1m);
        var spot_308 = await ws.Spot.GetOpenOrdersAsync("BTCUSDT");
        var spot_309 = await ws.Spot.CancelOrdersAsync("BTCUSDT");

        // Spot Web Socket API > Account Query Methods (PRIVATE)
        var spot_401 = await ws.Spot.GetAccountAsync();
        var spot_402 = await ws.Spot.GetRateLimitsAsync();
        var spot_403 = await ws.Spot.GetOrdersAsync("BTCUSDT");
        var spot_404 = await ws.Spot.GetOcoOrdersAsync();
        var spot_405 = await ws.Spot.GetUserTradesAsync("BTCUSDT");
        var spot_406 = await ws.Spot.GetPreventedTradesAsync("BTCUSDT", orderId: 100000001);
        */

        Console.WriteLine("Done!..");
        Console.ReadLine();
    }

    static async Task WebSocketApiExamplesAsync()
    {
        // WebSocket API Client
        var ws = new CoinTRSocketApiClient();
        ws.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX", "XXXXXXXX-API-PASSPHRASE-XXXXXXXX");

        /*
        // Subscription Samples
        var sub01 = await ws.Spot.SubscribeToAggregatedTradesAsync("BTCUSDT", (data) =>
        {
            Console.WriteLine($"{data.Data.Symbol} {data.Data.Price} {data.Data.Quantity} {data.Data.TradeTime}");
        });

        var sub02 = await ws.Spot.SubscribeToAggregatedTradesAsync(["ETHUSDT", "XRPUSDT"], (data) =>
        {
            Console.WriteLine($"{data.Data.Symbol} {data.Data.Price} {data.Data.Quantity} {data.Data.TradeTime}");
        });

        // Unsubscription Methods
        await ws.Spot.UnsubscribeAsync(sub01.Data);
        await ws.Spot.UnsubscribeAsync(sub02.Data.Id);
        await ws.Spot.UnsubscribeAllAsync();

        // Spot Web Socket Stream > Market Data Subscriptions (PUBLIC)
        await ws.Spot.SubscribeToAggregatedTradesAsync("BTCUSDT", (data) => { });
        await ws.Spot.SubscribeToAggregatedTradesAsync(["ETHUSDT", "XRPUSDT"], (data) => { });
        await ws.Spot.SubscribeToTradesAsync("BTCUSDT", (data) => { });
        await ws.Spot.SubscribeToTradesAsync(["ETHUSDT", "XRPUSDT"], (data) => { });
        await ws.Spot.SubscribeToKlinesAsync("BTCUSDT", BinanceKlineInterval.OneDay, (data) => { });
        await ws.Spot.SubscribeToKlinesAsync(["ETHUSDT", "XRPUSDT"], BinanceKlineInterval.OneDay, (data) => { });
        await ws.Spot.SubscribeToKlinesAsync("BTCUSDT", [BinanceKlineInterval.OneDay, BinanceKlineInterval.FourHours,], (data) => { });
        await ws.Spot.SubscribeToKlinesAsync(["ETHUSDT", "XRPUSDT"], [BinanceKlineInterval.OneDay, BinanceKlineInterval.FourHours,], (data) => { });
        await ws.Spot.SubscribeToMiniTickersAsync("BTCUSDT", (data) => { });
        await ws.Spot.SubscribeToMiniTickersAsync(["ETHUSDT", "XRPUSDT"], (data) => { });
        await ws.Spot.SubscribeToMiniTickersAsync((data) => { });
        await ws.Spot.SubscribeToTickersAsync("BTCUSDT", (data) => { });
        await ws.Spot.SubscribeToTickersAsync(["ETHUSDT", "XRPUSDT"], (data) => { });
        await ws.Spot.SubscribeToTickersAsync((data) => { });
        await ws.Spot.SubscribeToRollingWindowTickersAsync("BTCUSDT", TimeSpan.FromMinutes(60), (data) => { });
        await ws.Spot.SubscribeToRollingWindowTickersAsync(TimeSpan.FromMinutes(60), (data) => { });
        await ws.Spot.SubscribeToBookTickersAsync("BTCUSDT", (data) => { });
        await ws.Spot.SubscribeToBookTickersAsync(["ETHUSDT", "XRPUSDT"], (data) => { });
        await ws.Spot.SubscribeToPartialOrderBooksAsync("BTCUSDT", 20, null, (data) => { });
        await ws.Spot.SubscribeToPartialOrderBooksAsync("BTCUSDT", 20, 100, (data) => { });
        await ws.Spot.SubscribeToPartialOrderBooksAsync("BTCUSDT", 20, 1000, (data) => { });
        await ws.Spot.SubscribeToOrderBooksAsync("BTCUSDT", null, (data) => { });
        await ws.Spot.SubscribeToOrderBooksAsync("BTCUSDT", 100, (data) => { });
        await ws.Spot.SubscribeToOrderBooksAsync("BTCUSDT", 1000, (data) => { });

        // Spot Web Socket Stream > User Data Subscriptions (PRIVATE)
        await ws.Spot.SubscribeToUserDataStreamAsync("-----LISTEN-KEY-----",
            onOrderUpdateMessage: (data) => { },
            onOcoOrderUpdateMessage: (data) => { },
            onAccountPositionMessage: (data) => { },
            onAccountBalanceUpdate: (data) => { },
            onBalanceLockUpdate: (data) => { },
            onUserDataStreamTerminated: (data) => { },
            onListenKeyExpired: (data) => { });
        */

        Console.WriteLine("Done!..");
        Console.ReadLine();
    }

}