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
}