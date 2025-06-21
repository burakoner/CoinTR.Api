namespace CoinTR.Api.Spot;

/// <summary>
/// The side of an order
/// </summary>
public enum CoinTRSpotOrderSide : byte
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("sell")]
    Sell = 2
}
