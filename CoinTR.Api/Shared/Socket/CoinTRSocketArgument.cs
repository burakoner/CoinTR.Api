namespace CoinTR.Api.Shared;

internal class CoinTRSocketArgument
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    [JsonProperty("instType")]
    public string InstrumentType { get; set; } = string.Empty;

    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;
}
