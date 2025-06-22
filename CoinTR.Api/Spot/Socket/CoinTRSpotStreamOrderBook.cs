namespace CoinTR.Api.Spot;

/// <summary>
/// The order book for a asset
/// </summary>
public record CoinTRSpotStreamOrderBook
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonIgnore]
    public string Symbol { get; set; } = "";

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Checksum for the order book
    /// </summary>
    [JsonProperty("checksum")]
    public long Checksum { get; set; }

    /// <summary>
    /// The list of bids
    /// </summary>
    public List<BinanceSpotOrderBookEntry> Bids { get; set; } = [];

    /// <summary>
    /// The list of asks
    /// </summary>
    public List<BinanceSpotOrderBookEntry> Asks { get; set; } = [];
}