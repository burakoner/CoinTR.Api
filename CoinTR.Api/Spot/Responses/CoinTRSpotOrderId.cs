namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Order ID
/// </summary>
public record CoinTRSpotOrderId
{
    /// <summary>
    /// Order Id
    /// </summary>
    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientOid")]
    public string ClientOrderId { get; set; } = "";
}
