namespace CoinTR.Api;

/// <summary>
/// CoinTR Address
/// </summary>
public class CoinTRAddress
{
    /// <summary>
    /// Spot Rest API Address
    /// </summary>
    public string SpotRestApiAddress { get; set; } = "";

    /// <summary>
    /// Spot WebSocket API Public Address
    /// </summary>
    public string SpotSocketApiPublicAddress { get; set; } = "";

    /// <summary>
    /// Spot WebSocket API Private Address
    /// </summary>
    public string SpotSocketApiPrivateAddress { get; set; } = "";

    /// <summary>
    /// The default addresses to connect
    /// </summary>
    public static CoinTRAddress Default { get; } = new CoinTRAddress
    {
        SpotRestApiAddress = "https://api.cointr.com",
        SpotSocketApiPublicAddress = "wss://ws.cointr.com/v2/ws/public",
        SpotSocketApiPrivateAddress = "wss://ws.cointr.com/v2/ws/private",
    };
}
