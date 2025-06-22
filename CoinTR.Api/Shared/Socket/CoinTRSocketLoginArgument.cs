namespace CoinTR.Api.Shared;

internal class CoinTRSocketLoginArgument
{
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; } = "";

    [JsonProperty("passphrase")]
    public string PassPhrase { get; set; } = "";

    [JsonProperty("timestamp")]
    public string Timestamp { get; set; } = "";

    [JsonProperty("sign")]
    public string Signature { get; set; } = "";
}
