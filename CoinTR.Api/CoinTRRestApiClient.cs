using CoinTR.Api.Spot;

namespace CoinTR.Api;

/// <summary>
/// Binance Rest API Client
/// </summary>
public sealed class CoinTRRestApiClient : RestApiClient
{
    // Internal
    internal ILogger Logger => this._logger;
    internal TimeSyncState TimeSyncState { get; } = new("CoinTR");
    internal CoinTRRestApiClientOptions ApiOptions => (CoinTRRestApiClientOptions)ClientOptions;

    /// <summary>
    /// Binance Spot Rest API Client
    /// </summary>
    public ICoinTRSpotRestClient Spot { get; }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CoinTRRestApiClient() : this(null, new())
    {
    }

    /// <summary>
    /// Constructor with logger
    /// </summary>
    /// <param name="logger">Logger</param>
    public CoinTRRestApiClient(ILogger logger) : this(logger, new())
    {
    }

    /// <summary>
    /// Constructor with options
    /// </summary>
    /// <param name="options">Binance Rest API Client Options</param>
    public CoinTRRestApiClient(CoinTRRestApiClientOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// Constructor with logger and options
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Binance Rest API Client Options</param>
    public CoinTRRestApiClient(ILogger? logger, CoinTRRestApiClientOptions options) : base(logger ?? BaseClient.LoggerFactory.CreateLogger(typeof(CoinTRRestApiClient)), options)
    {
        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;

        Spot = new CoinTRSpotRestClient(this);
    }

    #region Public Nethods
    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="apikey">API Key</param>
    /// <param name="secret">API Secret</param>
    public void SetApiCredentials(string apikey, string secret) => SetApiCredentials(new ApiCredentials(apikey, secret));
    #endregion

    #region Overrided Methods
    /// <inheritdoc/>
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => new CoinTRAuthentication((CoinTRApiCredentials)credentials);

    /// <inheritdoc/>
    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync() => Spot.GetTimeAsync();

    /// <inheritdoc/>
    protected override TimeSyncInfo GetTimeSyncInfo() => new(Logger, ApiOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval, TimeSyncState);

    /// <inheritdoc/>
    protected override TimeSpan GetTimeOffset() => TimeSyncState.TimeOffset;

    /// <inheritdoc/>
    protected override Error ParseErrorResponse(JToken error)
    {
        if (!error.HasValues)
            return new ServerError(error.ToString());

        if (error["msg"] == null && error["code"] == null)
            return new ServerError(error.ToString());

        if (error["msg"] != null && error["code"] == null)
            return new ServerError((string)error["msg"]!);

        return new ServerError((int)error["code"]!, (string)error["msg"]!);
    }
    #endregion

    #region Internal Methods
    internal int? ReceiveWindow(int? receiveWindow) => receiveWindow ?? (ApiOptions.ReceiveWindow != null ? System.Convert.ToInt32(ApiOptions.ReceiveWindow?.TotalMilliseconds) : null);

    internal async Task<RestCallResult<T>> RequestAsync<T>(
        Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false,
        Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null,
        ArraySerialization? serialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        var result = await SendRequestAsync<CoinTRRestResponse<T>>(uri, method, cancellationToken, signed, queryParameters ?? [], bodyParameters ?? [], headerParameters ?? [], serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result && result.Error!.Code == -1021 && ApiOptions.AutoTimestamp)
        {
            Logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
            TimeSyncState.LastSyncTime = DateTime.MinValue;
        }

        if (!result) return result.AsError<T>(result.Error!);
        if (string.IsNullOrEmpty(result.Data.Code)) return result.AsError<T>(new ServerError("Invalid code returned from server"));
        if (result.Data == null) return result.AsError<T>(new ServerError("No data returned from server"));
        if (!int.TryParse(result.Data.Code, out var code)) return result.AsError<T>(new ServerError($"Invalid code returned from server: {result.Data.Code}"));
        if (code != 0) return result.AsError<T>(new ServerError(code, result.Data.Message ?? "No error message provided"));
        if (result.Data.Data == null) return result.AsError<T>(new ServerError("No data returned from server"));

        return result.As(result.Data.Data);
    }
    #endregion
}
