﻿namespace CoinTR.Api.Spot;

/// <summary>
/// Prevented order info
/// </summary>
public record BinanceSpotPreventedTrade
{
    /// <summary>
    /// Symbol
    /// </summary>
    public string Symbol { get; set; } = "";

    /// <summary>
    /// Match id
    /// </summary>
    public long PreventedMatchId { get; set; }

    /// <summary>
    /// Taker order id
    /// </summary>
    public long TakerOrderId { get; set; }

    /// <summary>
    /// Maker order id
    /// </summary>
    public long MakerOrderId { get; set; }

    /// <summary>
    /// Trade group id
    /// </summary>
    public long TradeGroupId { get; set; }

    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(MapConverter))]
    public CoinTRSpotSelfTradePreventionMode SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Trade price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Prevented quantity
    /// </summary>
    public decimal MakerPreventedQuantity { get; set; }

    /// <summary>
    /// Transaction time
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonProperty("transactTime")]
    public DateTime TransactionTime { get; set; }
}
