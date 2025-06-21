namespace CoinTR.Api.Spot;

/// <summary>
/// Order type for a spot order
/// </summary>
public enum CoinTRSpotOrderType : byte
{
    /// <summary>
    /// Limit orders will be placed at a specific price. If the price isn't available in the order book for that asset the order will be added in the order book for someone to fill.
    /// </summary>
    [Map("limit")]
    Limit = 1,

    /// <summary>
    /// Market order will be placed without a price. The order will be executed at the best price available at that time in the order book.
    /// </summary>
    [Map("market")]
    Market = 2,
}
