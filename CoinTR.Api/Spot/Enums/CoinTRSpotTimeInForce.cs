namespace CoinTR.Api.Spot;

/// <summary>
/// The time the order will be active for
/// </summary>
public enum CoinTRSpotTimeInForce : byte
{
    /// <summary>
    /// GoodTillCanceled orders will stay active until they are filled or canceled
    /// </summary>
    [Map("gtc")]
    GoodTillCanceled = 1,

    /// <summary>
    /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel = 2,

    /// <summary>
    /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
    /// </summary>
    [Map("fok")]
    FillOrKill = 3,

    /// <summary>
    /// Post Only
    /// </summary>
    [Map("post_only")]
    PostOnly = 4,
}
