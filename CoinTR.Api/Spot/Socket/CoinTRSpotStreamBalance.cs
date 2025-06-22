namespace CoinTR.Api.Spot;

public record CoinTRSpotStreamBalance
{
    /// <summary>
    /// Token name
    /// </summary>
    [JsonProperty("coin")]
    public string Asset { get; set; } = "";

    /// <summary>
    /// Available assets
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Amount of frozen assets
    /// Usually frozen when the limit order is placed or join the Launchpad
    /// </summary>
    [JsonProperty("frozen")]
    public decimal Frozen { get; set; }

    /// <summary>
    /// Amount of locked assets
    /// Locked assests required to become a fiat merchants, etc.
    /// </summary>
    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    /// <summary>
    /// Restricted availability
    /// For spot copy trading
    /// </summary>
    [JsonProperty("limitAvailable")]
    public decimal LimitAvailable { get; set; }

    /// <summary>
    /// Update time(ms)
    /// </summary>
    [JsonProperty("uTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }
}