namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Trade Scope
/// </summary>
public enum CoinTRSpotTradeScope : byte
{
    [Map("T")]
    Taker = 1,

    [Map("M")]
    Maker = 2,
}
