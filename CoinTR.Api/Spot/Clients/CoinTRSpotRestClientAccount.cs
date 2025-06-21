﻿namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    public Task<RestCallResult<BinanceSpotAccount>> GetAccountAsync(bool? omitZeroBalances = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("omitZeroBalances", omitZeroBalances?.ToString().ToLowerInvariant());
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<BinanceSpotAccount>(GetUrl(api, v2, "account"), HttpMethod.Get, ct, true, queryParameters: parameters, requestWeight: 20);
    }

    public Task<RestCallResult<List<BinanceSpotUserTrade>>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        symbol.ValidateBinanceSymbol();
        limit?.ValidateIntBetween(nameof(limit), 1, 1000);

        var parameters = new ParameterCollection
        {
            { "symbol", symbol }
        };
        parameters.AddOptional("orderId", orderId?.ToString(CoinTRConstants.CI));
        parameters.AddOptional("limit", limit?.ToString(CoinTRConstants.CI));
        parameters.AddOptional("fromId", fromId?.ToString(CoinTRConstants.CI));
        parameters.AddOptionalMilliseconds("startTime", startTime);
        parameters.AddOptionalMilliseconds("endTime", endTime);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        var weight = orderId.HasValue ? 5 : 20;
        return RequestAsync<List<BinanceSpotUserTrade>>(GetUrl(api, v2, "myTrades"), HttpMethod.Get, ct, true, queryParameters: parameters, requestWeight: weight);
    }

    public Task<RestCallResult<List<BinanceSpotOrderRateLimit>>> GetRateLimitsAsync(int? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<BinanceSpotOrderRateLimit>>(GetUrl(api, v2, "rateLimit/order"), HttpMethod.Get, ct, true, queryParameters: parameters, requestWeight: 20);
    }

    public Task<RestCallResult<List<BinanceSpotPreventedTrade>>> GetPreventedTradesAsync(string symbol, long? orderId = null, long? preventedMatchId = null, long? fromPreventedMatchId = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection()
        {
            { "symbol", symbol }
        };
        parameters.AddOptional("orderId", orderId);
        parameters.AddOptional("preventedMatchId", preventedMatchId);
        parameters.AddOptional("fromPreventedMatchId", fromPreventedMatchId);
        parameters.AddOptional("size", limit);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        var weight = preventedMatchId.HasValue ? 2 : 20;
        if (orderId.HasValue) weight = 20;
        return RequestAsync<List<BinanceSpotPreventedTrade>>(GetUrl(api, v2, "myPreventedMatches"), HttpMethod.Get, ct, true, queryParameters: parameters, requestWeight: weight);
    }
}