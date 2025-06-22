namespace CoinTR.Api.Spot;

public partial class CoinTRSpotRestClient
{
    public Task<RestCallResult<CoinTRSpotAccount>> GetAccountAsync(CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        return RequestAsync<CoinTRSpotAccount>(GetUrl(api, v2, "spot/account/info"), HttpMethod.Get, ct, true, queryParameters: []);
    }

    public Task<RestCallResult<List<CoinTRSpotBalance>>> GetBalancesAsync(string? asset = null, string? assetType = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("coin", asset);
        parameters.AddOptional("assetType", assetType);

        return RequestAsync<List<CoinTRSpotBalance>>(GetUrl(api, v2, "spot/account/assets"), HttpMethod.Get, ct, true, queryParameters: parameters);
    }
}