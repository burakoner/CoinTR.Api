namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    /*
    public Task<UnifiedResponse<Order>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, OrderTimeInForce timeInForce, decimal? price = null, decimal? stopPrice = null, decimal? quantity = null, decimal? quoteQuantity = null, string clientOrderId = null, CancellationToken ct = default);
    public Task<UnifiedResponse<bool>> CancelOrderAsync(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
    public Task<UnifiedResponse<bool>> CancelOpenOrdersAsync(string symbol, CancellationToken ct = default);
    public Task<UnifiedResponse<Order>> GetOrderAsync(string symbol, long? orderId = null, string clientOrderId = null, CancellationToken ct = default);
    public Task<UnifiedResponse<List<Order>>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default);
    public Task<UnifiedResponse<List<Order>>> GetOrderHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int limit = 500, CancellationToken ct = default);
    public Task<UnifiedResponse<List<Balance>>> GetBalancesAsync(CancellationToken ct = default);
    
    Task<int> SubscribeToOrderBookUpdatesAsync(string symbol, Action<OrderBook> onMessage, CancellationToken ct=default);
    Task<int> SubscribeToOrderUpdatesAsync(Action<OrderUpdate> onOrderUpdate, CancellationToken ct = default);
    */

    public Task<RestCallResult<BinanceSpotOrder>> PlaceOrderAsync(
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

        // parameters.AddOptionalString("requestTime", );
        parameters.AddOptional("receiveWindow", _.ReceiveWindow(receiveWindow));
        parameters.AddOptionalEnum("stpMode", selfTradePreventionMode);

        return RequestAsync<BinanceSpotOrder>(GetUrl(api, v2, "spot/trade/place-order"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<BinanceSpotOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        symbol.ValidateBinanceSymbol();
        if (orderId == null && origClientOrderId == null)
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptional("orderId", orderId);
        parameters.AddOptional("origClientOrderId", origClientOrderId);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<BinanceSpotOrder>(GetUrl(api, v2, "order"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<BinanceSpotOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, BinanceSpotOrderCancelRestriction? cancelRestriction = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        symbol.ValidateBinanceSymbol();
        if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
            throw new ArgumentException("Either orderId or origClientOrderId must be sent");

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptional("orderId", orderId);
        parameters.AddOptional("origClientOrderId", origClientOrderId);
        parameters.AddOptional("newClientOrderId", newClientOrderId);
        parameters.AddOptionalEnum("cancelRestrictions", cancelRestriction);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<BinanceSpotOrder>(GetUrl(api, v2, "order"), HttpMethod.Delete, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<BinanceSpotOrder>>> CancelOrdersAsync(string symbol, int? receiveWindow = null, CancellationToken ct = default)
    {
        symbol.ValidateBinanceSymbol();

        var parameters = new ParameterCollection
        {
            { "symbol", symbol }
        };
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<BinanceSpotOrder>>(GetUrl(api, v2, "openOrders"), HttpMethod.Delete, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<BinanceSpotOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("symbol", symbol);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<BinanceSpotOrder>>(GetUrl(api, v2, "openOrders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<BinanceSpotOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 1000);

        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddOptional("orderId", orderId);
        parameters.AddOptional("startTime", startTime?.ConvertToMilliseconds());
        parameters.AddOptional("endTime", endTime?.ConvertToMilliseconds());
        parameters.AddOptional("limit", limit);
        parameters.AddOptional("recvWindow", _.ReceiveWindow(receiveWindow));

        return RequestAsync<List<BinanceSpotOrder>>(GetUrl(api, v2, "allOrders"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

}