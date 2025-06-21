namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Symbol Status
/// </summary>
public enum CoinTRSpotSymbolStatus : byte
{
    /// <summary>
    /// Online
    /// </summary>
    [Map("online")]
    Online = 1,

    /// <summary>
    /// Offline
    /// </summary>
    [Map("offline")]
    Offline = 2,

    /// <summary>
    /// Gray
    /// </summary>
    [Map("gray")]
    Gray = 3,

    /// <summary>
    /// Halt
    /// </summary>
    [Map("halt")]
    Halt = 4,
}
