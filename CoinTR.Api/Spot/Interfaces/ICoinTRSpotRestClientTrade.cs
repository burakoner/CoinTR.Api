namespace CoinTR.Api.Spot;

/// <summary>
/// Interface for the CoinTR Spot REST API Client Trading Methods
/// </summary>
public interface ICoinTRSpotRestClientTrade
{
    Task<RestCallResult<CoinTRSpotOrderId>> PlaceOrderAsync(
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
        CancellationToken ct = default);

    Task<RestCallResult<CoinTRSpotOrderId>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, bool triggerOrder = false, CancellationToken ct = default);

    Task<RestCallResult<CoinTRSpotOrderSymbol>> CancelOrdersAsync(string symbol, CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotOrder>>> GetOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, int? receiveWindow = null, CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
}