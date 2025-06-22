namespace CoinTR.Api.Shared;

internal class CoinTRSocketArgument
{
    [JsonProperty("coin", NullValueHandling = NullValueHandling.Ignore)]
    public string? Asset { get; set; }

    [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
    public string? InstrumentId { get; set; }

    [JsonProperty("instType")]
    public string InstrumentType { get; set; } = string.Empty;

    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;
}
