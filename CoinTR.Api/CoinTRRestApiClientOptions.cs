namespace CoinTR.Api;

/// <summary>
/// CoinTR Rest API Client Options
/// </summary>
public class CoinTRRestApiClientOptions : RestApiClientOptions
{
    /// <summary>
    /// Receive Window
    /// </summary>
    public TimeSpan? ReceiveWindow { get; set; }

    /// <summary>
    /// Whether or not to automatically sync the local time with the server time
    /// </summary>
    public bool AutoTimestamp { get; set; } = true;

    /// <summary>
    /// How often the timestamp adjustment between client and server is recalculated. If you need a very small TimeSpan here you're probably better of syncing your server time more often
    /// </summary>
    public TimeSpan TimestampRecalculationInterval { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Constructor
    /// </summary>
    public CoinTRRestApiClientOptions() : this("", "", "")
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="apikey">API Key</param>
    /// <param name="secret">API Secret</param>
    /// <param name="phrase">API Pass Phrase</param>
    public CoinTRRestApiClientOptions(string apikey, string secret, string phrase) : this(new CoinTRApiCredentials(apikey, secret, phrase))
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="credentials">API Credentials</param>
    public CoinTRRestApiClientOptions(ApiCredentials credentials)
    {
        // API Credentials
        ApiCredentials = credentials;
    }
}
