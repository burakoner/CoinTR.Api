namespace CoinTR.Api.Spot;

/// <summary>
/// Self trade prevention mode
/// </summary>
public enum CoinTRSpotSelfTradePreventionMode : byte
{
    /// <summary>
    /// None
    /// </summary>
    [Map("none")]
    None = 0,

    /// <summary>
    /// Expire taker
    /// </summary>
    [Map("cancel_taker")]
    CancelTaker = 1,

    /// <summary>
    /// Expire maker
    /// </summary>
    [Map("cancel_maker")]
    CancelMaker = 2,

    /// <summary>
    /// Expire both
    /// </summary>
    [Map("cancel_both")]
    CancelBoth = 3,
}
