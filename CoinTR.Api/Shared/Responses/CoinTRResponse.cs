namespace CoinTR.Api.Shared;

/// <summary>
/// CoinTR Response
/// </summary>
internal record CoinTRResponse<T>
{
    /// <summary>
    /// Code
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("msg")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Request Time
    /// </summary>
    [JsonProperty("requestTime")]
    public DateTime RequestTime { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data")]
    public T? Data { get; set; }
}