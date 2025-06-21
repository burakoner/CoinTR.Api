namespace CoinTR.Api;

internal class CoinTRAuthentication(CoinTRApiCredentials credentials) : AuthenticationProvider(credentials ?? new CoinTRApiCredentials("", "", ""))
{
    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // Check Point
        if (Credentials == null || Credentials.Key == null) return;

        // Check Point
        if (!signed) return;

        // Check Point
        if (Credentials is null || 
            Credentials.Key is null || 
            Credentials.Secret is null || 
            ((CoinTRApiCredentials)Credentials).PassPhrase is null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Api Key
        var apikey = Credentials.Key.GetString();
        if (string.IsNullOrEmpty(apikey)) throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Api Pass Phrase
        var phrase = ((CoinTRApiCredentials)Credentials).PassPhrase.GetString();
        if (string.IsNullOrEmpty(phrase)) throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Set Uri Parameters
        uri = uri.SetParameters(query, serialization);

        // Timestamp
        var timestamp = GetMillisecondTimestamp(apiClient);
        var methodName = method.ToString().ToUpperInvariant();

        // Signature
        var signature = string.Empty;
        if (Credentials.Type == ApiCredentialsType.HMAC)
        {
            if (method == HttpMethod.Get)
            {
                var signbody = timestamp + methodName + uri.PathAndQuery;
                signature = SignHMACSHA256(signbody, SignatureOutputType.Base64);
            }
            else if (method == HttpMethod.Post)
            {
                var signbody = timestamp + methodName + uri.AbsolutePath + bodyContent;
                signature = SignHMACSHA256(signbody, SignatureOutputType.Base64);
            }
            else
            {
                throw new NotImplementedException($"Method {method} is not implemented for authentication in CoinTR API.");
            }
        }
        else
        {
            throw new NotImplementedException("Only HMAC authentication is currently supported in CoinTR API.");
        }

        // Headers
        headers.Add("ACCESS-KEY", apikey);
        headers.Add("ACCESS-SIGN", signature);
        headers.Add("ACCESS-PASSPHRASE", phrase);
        headers.Add("ACCESS-TIMESTAMP", timestamp);
        headers.Add("locale", "en-US");
    }

    public Dictionary<string, object> AuthenticateSocketParameters(Dictionary<string, object> providedParameters, long timestamp)
    {
        throw new NotImplementedException("Socket authentication is not implemented for CoinTR API.");
    }
}
