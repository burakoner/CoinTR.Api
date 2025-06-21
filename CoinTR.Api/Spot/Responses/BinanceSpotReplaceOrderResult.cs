﻿namespace CoinTR.Api.Spot;

/// <summary>
/// The result of replacing an order
/// </summary>
public record BinanceSpotReplaceOrderResult : BinanceReplaceResult
{
    /// <summary>
    /// Cancel result
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    public BinanceSpotOrderOperationResult CancelResult { get; set; }

    /// <summary>
    /// New order result
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    public BinanceSpotOrderOperationResult NewOrderResult { get; set; }

    /// <summary>
    /// Cancel order response. Make sure to check that the CancelResult is Success, else the CancelResponse.Message will contain more info
    /// </summary>
    public BinanceReplaceCancelOrder CancelResponse { get; set; } = default!;

    /// <summary>
    /// New order response. Make sure to check that the NewOrderResult is Success, else the NewOrderResponse.Message will contain more info
    /// </summary>
    public BinanceReplaceOrder NewOrderResponse { get; set; } = default!;
}

/// <summary>
/// Replace order
/// </summary>
public record BinanceReplaceOrder : BinancePlacedOrder
{
    /// <summary>
    /// Failure message
    /// </summary>
    [JsonProperty("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Error code if not successful
    /// </summary>
    public int? Code { get; set; }
}

/// <summary>
/// Replace cancel order info
/// </summary>
public record BinanceReplaceCancelOrder : BinanceSpotOrderBase
{
    /// <summary>
    /// Failure message
    /// </summary>
    [JsonProperty("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Error code if not successful
    /// </summary>
    public int? Code { get; set; }
}

/// <summary>
/// Replace result
/// </summary>
public record BinanceReplaceResult
{
    /// <summary>
    /// Failure message
    /// </summary>
    [JsonProperty("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Error code if not successful
    /// </summary>
    public int? Code { get; set; }
}
