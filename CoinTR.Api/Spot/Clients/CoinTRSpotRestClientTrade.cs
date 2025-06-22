namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    public Task<RestCallResult<CoinTRSpotOrderId>> PlaceOrderAsync(
        string symbol,
        CoinTRSpotOrderSide side,
        CoinTRSpotOrderType type,
        CoinTRSpotTimeInForce force,

        decimal quantity,
        decimal? price = null,
        string? clientOrderId = null,

        bool triggerOrder = false,
        decimal? triggerPrice = null,
        int? receiveWindow = null,
        CoinTRSpotSelfTradePreventionMode? selfTradePreventionMode = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddEnum("side", side);
        parameters.AddEnum("orderType", type);
        parameters.AddEnum("force", force);

        parameters.AddOptionalString("price", price);
        parameters.AddString("size", quantity);
        parameters.AddOptional("clientOid", clientOrderId);

        parameters.AddOptionalString("triggerPrice", triggerPrice);
        parameters.AddOptional("tpslType", triggerOrder ? "tpsl" : "normal");

        parameters.AddOptionalEnum("stpMode", selfTradePreventionMode);

        parameters.AddOptionalString("receiveWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<CoinTRSpotOrderId>(GetUrl(api, v2, "spot/trade/place-order"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<CoinTRSpotOrderId>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, bool triggerOrder = false, CancellationToken ct = default)
    {
        if (!orderId.HasValue && string.IsNullOrEmpty(clientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptionalString("orderId", orderId);
        parameters.AddOptional("clientOid", clientOrderId);
        parameters.AddOptional("tpslType", triggerOrder ? "tpsl" : "normal");

        return RequestAsync<CoinTRSpotOrderId>(GetUrl(api, v2, "spot/trade/cancel-order"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<CoinTRSpotOrderSymbol>> CancelOrdersAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "symbol", symbol }
        };

        return RequestAsync<CoinTRSpotOrderSymbol>(GetUrl(api, v2, "spot/trade/cancel-symbol-order"), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<CoinTRSpotOrder>> GetOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        if (orderId == null && clientOrderId == null)
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptionalString("orderId", orderId);
        parameters.AddOptional("clientOid", clientOrderId);
        parameters.AddOptionalString("receiveWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<CoinTRSpotOrder>(GetUrl(api, v2, "spot/trade/orderInfo"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<CoinTRSpotOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", symbol);
        parameters.AddOptionalString("receiveWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<CoinTRSpotOrder>>(GetUrl(api, v2, "spot/trade/unfilled-orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<CoinTRSpotOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 1000);

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptionalString("orderId", orderId);
        parameters.AddOptionalString("startTime", startTime?.ConvertToMilliseconds());
        parameters.AddOptionalString("endTime", endTime?.ConvertToMilliseconds());
        parameters.AddOptionalString("limit", limit);
        parameters.AddOptionalString("receiveWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<CoinTRSpotOrder>>(GetUrl(api, v2, "spot/trade/history-orders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

}