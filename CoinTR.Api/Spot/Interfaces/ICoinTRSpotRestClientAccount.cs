namespace CoinTR.Api.Spot;

/// <summary>
/// Interface for the CoinTR Spot REST API Client Account Methods
/// </summary>
public interface ICoinTRSpotRestClientAccount
{
    Task<RestCallResult<CoinTRSpotAccount>> GetAccountAsync(CancellationToken ct = default);

    Task<RestCallResult<List<CoinTRSpotBalance>>> GetBalancesAsync(string? asset = null, string? assetType = null, CancellationToken ct = default);
}