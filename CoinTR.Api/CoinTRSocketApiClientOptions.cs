namespace CoinTR.Api;

/// <summary>
/// CoinTR WebSocket API Client Options
/// </summary>
public class CoinTRSocketApiClientOptions : WebSocketApiClientOptions
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
}