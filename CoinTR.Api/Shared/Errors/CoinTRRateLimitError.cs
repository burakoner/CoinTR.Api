namespace CoinTR.Api.Shared;

/// <summary>
/// CoinTR rate limit error
/// </summary>
public class CoinTRRateLimitError : ServerRateLimitError
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="message"></param>
    public CoinTRRateLimitError(string message) : base(message)
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    public CoinTRRateLimitError(int? code, string message, object? data) : base(code, message, data)
    {
    }
}
