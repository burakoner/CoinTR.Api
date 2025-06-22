namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Order Symbol
/// </summary>
public record CoinTRSpotOrderSymbol
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = "";
}
