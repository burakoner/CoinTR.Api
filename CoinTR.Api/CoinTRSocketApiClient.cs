using CoinTR.Api.Spot;

namespace CoinTR.Api;

/// <summary>
/// CoinTR WebSocket API Client
/// </summary>
public class CoinTRSocketApiClient
{
    // Internal
    internal ILogger Logger { get; }
    internal TimeSyncState TimeSyncState { get; } = new("CoinTR");
    internal CoinTRSocketApiClientOptions ApiOptions { get; }
    internal CoinTRRestApiClient RestApiClient { get; }

    /// <summary>
    /// CoinTR Spot WebSocket API Client
    /// </summary>
    public ICoinTRSpotSocketClient Spot { get; }

    /// <summary>
    /// CoinTR WebSocket API Client Constructor
    /// </summary>
    public CoinTRSocketApiClient() : this(null, new())
    {
    }

    /// <summary>
    /// Constructor with logger
    /// </summary>
    /// <param name="logger">Logger</param>
    public CoinTRSocketApiClient(ILogger logger) : this(logger, new())
    {
    }

    /// <summary>
    /// CoinTR WebSocket API Client Constructor
    /// </summary>
    /// <param name="options"></param>
    public CoinTRSocketApiClient(CoinTRSocketApiClientOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// CoinTR WebSocket API Client Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Web Socket API Options</param>
    public CoinTRSocketApiClient(ILogger? logger, CoinTRSocketApiClientOptions options)
    {
        ApiOptions = options;
        Logger = logger ?? BaseClient.LoggerFactory.CreateLogger<CoinTRSocketApiClient>();
        RestApiClient = new(Logger, new());

        Spot = new CoinTRSpotSocketClient(this);
    }

    internal int? ReceiveWindow(int? receiveWindow) => receiveWindow ?? (ApiOptions.ReceiveWindow != null ? System.Convert.ToInt32(ApiOptions.ReceiveWindow?.TotalMilliseconds) : null);

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="apikey"></param>
    /// <param name="secret"></param>
    /// <param name="phrase"></param>
    public void SetApiCredentials(string apikey, string secret, string phrase)
    {
        ((CoinTRSpotSocketClient)Spot).SetApiCredentials(new CoinTRApiCredentials(apikey, secret, phrase));
    }

}
