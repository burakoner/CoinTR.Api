namespace CoinTR.Api.Shared;

internal record CoinTRServerTime
{
    [JsonProperty("serverTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ServerTime { get; set; }
}