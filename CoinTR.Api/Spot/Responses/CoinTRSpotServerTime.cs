namespace CoinTR.Api.Spot;

internal record CoinTRSpotServerTime
{
    [JsonProperty("serverTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ServerTime { get; set; }
}