namespace CoinTR.Api.Spot;

/// <summary>
/// The status of an order
/// </summary>
public enum CoinTRSpotOrderStatus : byte
{
    /// <summary>
    /// Initial state of the order, when it is created but not yet processed
    /// </summary>
    [Map("init")]
    Init = 0,

    /// <summary>
    /// New order has been created and is waiting to be processed
    /// </summary>
    [Map("new")]
    New = 1,

    /// <summary>
    /// Live order is currently active and can be filled
    /// </summary>
    [Map("live")]
    Live = 2,

    /// <summary>
    /// Order is partly filled, still has quantity left to fill
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 3,

    /// <summary>
    /// The order has been filled and completed
    /// </summary>
    [Map("filled")]
    Filled = 4,

    /// <summary>
    /// The order has been canceled
    /// </summary>
    [Map("cancelled")]
    Canceled = 5,
}
