namespace CoinTR.Api.Shared;

internal class CoinTRSocketLoginRequest
{
    [JsonProperty("op")]
    public string Operation { get; set; } = string.Empty;

    [JsonProperty("args")]
    public CoinTRSocketLoginArgument[] Arguments { get; set; } = [];
}