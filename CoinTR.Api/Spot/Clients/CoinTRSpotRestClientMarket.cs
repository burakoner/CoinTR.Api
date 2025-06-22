namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    public Task<RestCallResult<List<CoinTRSpotTicker>>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", symbol);

        return RequestAsync<List<CoinTRSpotTicker>>(GetUrl(api, v2, "spot/market/tickers"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    public Task<RestCallResult<CoinTRSpotOrderBook>> GetOrderBookAsync(string symbol, string? type = null, int? limit = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 150);

        var parameters = new ParameterCollection
        {
            { "symbol", symbol }
        };
        parameters.AddOptionalString("limit", limit);

        return RequestAsync<CoinTRSpotOrderBook>(GetUrl(api, v2, "spot/market/orderbook"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }
}