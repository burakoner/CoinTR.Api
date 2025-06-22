namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Ticker
/// </summary>
public record CoinTRSpotStreamTicker
{
    /// <summary>
    /// The symbol the price is for
    /// </summary>
    [JsonProperty("instId")]
    public string Symbol { get; set; } = "";
    
    /// <summary>
    /// The most recent trade price
    /// </summary>
    [JsonProperty("lastPr")]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// The open price 24 hours ago
    /// </summary>
    [JsonProperty("open24h")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// The highest price in the last 24 hours
    /// </summary>
    [JsonProperty("high24h")]
    public decimal HighPrice { get; set; }

    /// <summary>
    /// The lowest price in the last 24 hours
    /// </summary>
    [JsonProperty("low24h")]
    public decimal LowPrice { get; set; }

    /// <summary>
    /// Change at UTC+0, 0.01 means 1%.
    /// </summary>
    [JsonProperty("change24h")]
    public decimal PriceChangePercent { get; set; }

    /// <summary>
    /// The best bid price in the order book
    /// </summary>
    [JsonProperty("bidPr")]
    public decimal? BestBidPrice { get; set; }

    /// <summary>
    /// The quantity of the best bid price in the order book
    /// </summary>
    [JsonProperty("bidSz")]
    public decimal? BestBidQuantity { get; set; }

    /// <summary>
    /// The best ask price in the order book
    /// </summary>
    [JsonProperty("askPr")]
    public decimal? BestAskPrice { get; set; }

    /// <summary>
    /// The quantity of the best ask price in the order book
    /// </summary>
    [JsonProperty("askSz")]
    public decimal? BestAskQuantity { get; set; }

    /// <summary>
    /// The base volume traded in the last 24 hours
    /// </summary>
    [JsonProperty("baseVolume")]
    public decimal BaseVolume { get; set; }

    /// <summary>
    /// The quote asset volume traded in the last 24 hours
    /// </summary>
    [JsonProperty("quoteVolume")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// UTC±00:00 Entry price
    /// </summary>
    [JsonProperty("openUtc")]
    public decimal OpenPriceUTC { get; set; }

    /// <summary>
    /// Current time Unix millisecond timestamp, e.g. 1690196141868
    /// </summary>
    [JsonProperty("ts")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// 24-hour change, 0.01 means 1%.
    /// </summary>
    [JsonProperty("changeUtc24h")]
    public decimal PriceChangePercentUTC { get; set; }
}
