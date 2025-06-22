namespace CoinTR.Api.Shared;

internal class CoinTRSocketResponse
{
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;

    [JsonProperty("arg")]
    public CoinTRSocketArgument Argument { get; set; } = default!;

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}

internal class CoinTRSocketResponse<T>: CoinTRSocketResponse
{
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}