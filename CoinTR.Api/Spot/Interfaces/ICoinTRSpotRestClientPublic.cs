namespace CoinTR.Api.Spot;

/// <summary>
/// Interface for the CoinTR Spot REST API Client General Methods
/// </summary>
public interface ICoinTRSpotRestClientPublic
{
    /// <summary>
    /// Requests the server for the local time
    /// <para><a href="https://www.cointr.com/api-doc/common/public/get-server-time" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Server time</returns>
    Task<RestCallResult<DateTime>> GetTimeAsync(CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotAsset>>> GetAssetsAsync(string? asset = null, CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotSymbol>>> GetSymbolsAsync(string? symbol = null, CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotTicker>>> GetTickersAsync(string? symbol = null, CancellationToken ct = default);

    Task<RestCallResult<CoinTRSpotOrderBook>> GetOrderBookAsync(string symbol, string? type = null, int? limit = null, CancellationToken ct = default);
}