namespace CoinTR.Api.Spot;

/// <summary>
/// The order book for a asset
/// </summary>
public record CoinTRSpotOrderBook
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
    /// The list of bids
    /// </summary>
    public List<CoinTRSpotOrderBookEntry> Bids { get; set; } = [];

    /// <summary>
    /// The list of asks
    /// </summary>
    public List<CoinTRSpotOrderBookEntry> Asks { get; set; } = [];
}

/// <summary>
/// An entry in the order book
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record CoinTRSpotOrderBookEntry
{
    /// <summary>
    /// The price of this order book entry
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity of this price in the order book
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }
}
