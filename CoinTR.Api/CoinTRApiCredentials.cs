namespace CoinTR.Api;

/// <summary>
/// CoinTR API Credentials
/// </summary>
/// <param name="apiKey">API Key</param>
/// <param name="apiSecret">API Secret</param>
/// <param name="passPhrase">Pass Phrase</param>
public class CoinTRApiCredentials(string apiKey, string apiSecret, string passPhrase) : ApiCredentials(apiKey, apiSecret)
{
    /// <summary>
    /// Pass Phrase
    /// </summary>
    public SensitiveString PassPhrase { get; } = passPhrase.ToSensitiveString();

    /// <summary>
    /// Copies and creates a new CoinTRApiCredentials instance
    /// </summary>
    /// <returns></returns>
    public override ApiCredentials Copy()
    {
        return new CoinTRApiCredentials(Key!.GetString(), Secret!.GetString(), PassPhrase!.GetString());
    }
}