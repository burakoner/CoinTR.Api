namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    public async Task<RestCallResult<DateTime>> GetTimeAsync(CancellationToken ct = default)
    {
        var result = await RequestAsync<CoinTRSpotServerTime>(GetUrl(api, v2, "public/time"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);

        return result.Success
            ? result.As(result.Data?.ServerTime ?? default)
            : result.AsError<DateTime>(result.Error!);
    }

    public Task<RestCallResult<List<CoinTRSpotAsset>>> GetAssetsAsync(string? asset = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("coin", asset);

        return RequestAsync<List<CoinTRSpotAsset>>(GetUrl(api, v2, "spot/public/coins"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<CoinTRSpotSymbol>>> GetSymbolsAsync(string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", symbol);

        return RequestAsync<List<CoinTRSpotSymbol>>(GetUrl(api, v2, "spot/public/symbols"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    public Task<RestCallResult<List<CoinTRSpotTicker>>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", symbol);

        return RequestAsync<List<CoinTRSpotTicker>>(GetUrl(api, v2, "spot/market/tickers"), HttpMethod.Get, ct, false, queryParameters: parameters);
    }

    public async Task<RestCallResult<CoinTRSpotOrderBook>> GetOrderBookAsync(string symbol, string? type = null, int? limit = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 150);

        var parameters = new ParameterCollection
        {
            { "symbol", symbol }
        };
        parameters.AddOptionalString("limit", limit);

        var result = await RequestAsync<CoinTRSpotOrderBook>(GetUrl(api, v2, "spot/market/orderbook"), HttpMethod.Get, ct, false, queryParameters: parameters);
        if(!result.Success||result.Data==null) return result;
        
        result.Data.Symbol = symbol;
        return result;
    }
}