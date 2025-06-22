namespace CoinTR.Api.Spot;

internal partial class CoinTRSpotRestClient(CoinTRRestApiClient root) : ICoinTRSpotRestClient
{
    // Api
    private const string v2 = "2";
    private const string api = "api";

    // Parent
    private CoinTRRestApiClient _ { get; } = root;

    // Internal
    private ILogger Logger => _.Logger;
    private CoinTRRestApiClientOptions RestOptions => _.ApiOptions;

    private Task<RestCallResult<T>> RequestAsync<T>(
        Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false,
        Dictionary<string, object>? queryParameters = null,
        Dictionary<string, object>? bodyParameters = null,
        Dictionary<string, string>? headerParameters = null,
        ArraySerialization? serialization = null,
        JsonSerializer? deserializer = null,
        bool ignoreRatelimit = false,
        int requestWeight = 1) where T : class
        => _.RequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight);

    private Uri GetUrl(string api, string version, string endpoint)
    {
        var url = CoinTRAddress.Default.SpotRestApiAddress;
        if (!string.IsNullOrEmpty(api)) url = url.AppendPath($"{api}");
        if (!string.IsNullOrEmpty(version)) url = url.AppendPath($"v{version}");
        if (!string.IsNullOrEmpty(endpoint)) url = url.AppendPath($"{endpoint}");

        return new Uri(url);
    }
}