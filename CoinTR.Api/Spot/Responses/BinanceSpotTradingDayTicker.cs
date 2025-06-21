﻿namespace CoinTR.Api.Spot;

/// <summary>
/// Price change stats for the current trading day
/// </summary>
public record BinanceSpotTradingDayTicker: BinanceTradingDayMiniTicker
{
    /// <summary>
    /// The actual price change in the last 24 hours
    /// </summary>
    public decimal PriceChange { get; set; }

    /// <summary>
    /// The price change in percentage in the last 24 hours
    /// </summary>
    public decimal PriceChangePercent { get; set; }

    /// <summary>
    /// The weighted average price in the last 24 hours
    /// </summary>
    [JsonProperty("weightedAvgPrice")]
    public decimal WeightedAveragePrice { get; set; }
}

/// <summary>
/// Price change stats for the current trading day
/// </summary>
public record BinanceTradingDayMiniTicker
{
    /// <summary>
    /// The symbol the price is for
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// The open price 24 hours ago
    /// </summary>
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// The highest price in the last 24 hours
    /// </summary>
    public decimal HighPrice { get; set; }

    /// <summary>
    /// The lowest price in the last 24 hours
    /// </summary>
    public decimal LowPrice { get; set; }

    /// <summary>
    /// The most recent trade price
    /// </summary>
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Volume in base asset
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Volume in quote asset
    /// </summary>
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// Time at which this stats opened
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime OpenTime { get; set; }

    /// <summary>
    /// Time at which this stats closed
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CloseTime { get; set; }

    /// <summary>
    /// The first trade ID in the last 24 hours
    /// </summary>
    [JsonProperty("firstId")]
    public long FirstTradeId { get; set; }

    /// <summary>
    /// The last trade ID in the last 24 hours
    /// </summary>
    [JsonProperty("lastId")]
    public long LastTradeId { get; set; }

    /// <summary>
    /// The amount of trades made in the last 24 hours
    /// </summary>
    [JsonProperty("count")]
    public long TotalTrades { get; set; }
}
