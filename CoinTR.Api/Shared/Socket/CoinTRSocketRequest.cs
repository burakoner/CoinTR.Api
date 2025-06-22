namespace CoinTR.Api.Shared;

internal class CoinTRSocketRequest
{
    [JsonProperty("op")]
    public string Operation { get; set; } = string.Empty;

    [JsonProperty("args")]
    public CoinTRSocketArgument[] Arguments { get; set; } = [];
}