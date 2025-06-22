namespace CoinTR.Api.Spot;

internal partial class CoinTRSpotSocketClient : WebSocketApiClient, ICoinTRSpotSocketClient
{
    // Internal
    internal ILogger Logger { get => _logger; }
    internal TimeSyncState TimeSyncState { get; } = new("CoinTR Spot WS");
    internal CallResult<T> Deserializer<T>(string data, JsonSerializer? serializer = null, int? requestId = null) => Deserialize<T>(data, serializer, requestId);
    internal CallResult<T> Deserializer<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null) => Deserialize<T>(obj, serializer, requestId);

    protected async Task<CallResult<DateTime>> GetServerTimestampAsync()
    {
        var result = await _.RestApiClient.Spot.GetTimeAsync();
        if (!result) return result.AsError<DateTime>(result.Error!);
        return result.As(result.Data);
    }
    protected TimeSyncInfo GetTimeSyncInfo() => new(Logger, SocketOptions.AutoTimestamp, SocketOptions.TimestampRecalculationInterval, TimeSyncState);
    protected TimeSpan GetTimeOffset() => TimeSyncState.TimeOffset;

    // Parent
    internal CoinTRSocketApiClient _ { get; }

    // Internal
    internal CoinTRSocketApiClientOptions SocketOptions => _.ApiOptions;

    internal CoinTRSpotSocketClient(CoinTRSocketApiClient root) : base(root.Logger, root.ApiOptions)
    {
        _ = root;

        RateLimitPerConnectionPerSecond = 4;
        SetDataInterpreter((data) => string.Empty, null);
    }

    #region Overrided Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => new CoinTRAuthentication((CoinTRApiCredentials)credentials);

    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T>? callResult)
    {
        throw new NotImplementedException();
    }

    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
    {
        callResult = null;

        if (message.Type != JTokenType.Object)
            return false;

        if (message["event"] == null) return false;
        if (message["event"]!.Type != JTokenType.String) return false;
        var evt = message["event"]!.Value<string>();
        if (evt != "subscribe" && evt != "error") return false;

        if (message["arg"] == null) return false;
        if (message["arg"]!.Type != JTokenType.Object) return false;
        var arg = Deserialize<CoinTRSocketArgument>(message["arg"]!);
        if (arg == null) return false;
        if (arg.Data == null) return false;
        if (arg.Success == false) return false;

        var argument = ((CoinTRSocketRequest)request).Arguments.FirstOrDefault(x => x.InstrumentId == arg.Data.InstrumentId && x.InstrumentType == arg.Data.InstrumentType && x.Channel == arg.Data.Channel);
        if (argument == null) return false;

        if (evt == "subscribe")
        {
            Logger.Log(LogLevel.Trace, $"Socket {connection.Id} Subscription completed");
            callResult = new CallResult<object>(new object());
            return true;
        }

        if (evt == "error")
        {
            var code = message["code"] != null ? message["code"]!.Value<int>() : -1;
            var error = message["msg"] != null ? message["msg"]!.Value<string>() : "Unknown Error!..";
            callResult = new CallResult<object>(new ServerError(code, error!));
            return true;
        }

        return true;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        var action = message["action"];
        if (action == null) return false;

        if (message["arg"] == null) return false;
        if (message["arg"]!.Type != JTokenType.Object) return false;
        var arg = Deserialize<CoinTRSocketArgument>(message["arg"]!);
        if (arg == null) return false;
        if (arg.Data == null) return false;
        if (arg.Success == false) return false;

        var argument = ((CoinTRSocketRequest)request).Arguments.FirstOrDefault(x => x.InstrumentId == arg.Data.InstrumentId && x.InstrumentType == arg.Data.InstrumentType && x.Channel == arg.Data.Channel);
        if (argument == null) return false;

        return true;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
    {
        return true;
    }

    protected override async Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
    {
        // Check Point
        //if (connection.Authenticated)
        //    return new CallResult<bool>(true, null);

        // Check Point
        var credentials = AuthenticationProvider.Credentials;
        if (credentials is null || credentials.Key is null || credentials.Secret is null || ((CoinTRApiCredentials)credentials).PassPhrase is null)
            return new CallResult<bool>(new NoApiCredentialsError());

        // Get Credentials
        var key = credentials.Key.GetString();
        var secret = credentials.Secret.GetString();
        var passphrase = ((CoinTRApiCredentials)credentials).PassPhrase.GetString();

        // Check Point
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
            return new CallResult<bool>(new NoApiCredentialsError());

        // Timestamp
        //var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(OkxConstants.OkxCultureInfo);
        var timestamp = DateTime.UtcNow.Add(GetTimeOffset()).ConvertToSeconds();

        // Signature
        var signtext = timestamp + "GET" + "/users/self/verify";
        var hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
        var signature = ((CoinTRAuthentication)AuthenticationProvider).AuthenticateSocketApi(timestamp);
        var request = new CoinTRSocketLoginRequest
        {
            Operation = "login",
            Arguments = [new CoinTRSocketLoginArgument
            {
                ApiKey = key,
                Passphrase = passphrase,
                Timestamp = timestamp.ToString(),
                Signature = signature,
            }]
        };

        // Try to Login
        var result = new CallResult<bool>(new ServerError("No response from server"));
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if (data == null) return false;
            if (data["event"] == null) return false;
            var evt = data["event"]!.Value<string>();
            if (string.IsNullOrEmpty(evt)) return false;

            if (evt == "error")
            {
                var code = data["code"] != null ? data["code"]!.Value<int>() : -1;
                var error = data["msg"] != null ? data["msg"]!.Value<string>() : "Unknown Error!..";
                Logger.Log(LogLevel.Warning, $"Authorization failed. [{code}] {error}");
                result = new CallResult<bool>(new ServerError(code, error!));
                return true;
            }
            else if (evt == "login")
            {
                var code = data["code"] != null ? data["code"]!.Value<int>() : -1;
                Logger.Log(LogLevel.Debug, "Authorization completed");
                result = new CallResult<bool>(code == 0);
                return true;
            }
            else
            {
                return false;
            }
        });

        return result;
    }

    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        var topics = ((CoinTRSocketRequest)subscription.Request!).Arguments;
        var request = new CoinTRSocketRequest { Operation = "unsubscribe", Arguments = topics };

        if (!connection.Connected)
            return true;

        await connection.SendAndWaitAsync(request, ClientOptions.ResponseTimeout, message =>
        {
            if (message.Type != JTokenType.Object)
                return false;

            if (message["event"] == null) return false;
            if (message["event"]!.Type != JTokenType.String) return false;
            var evt = message["event"]!.Value<string>();
            if (evt != "unsubscribe" && evt != "error") return false;

            if (message["arg"] == null) return false;
            if (message["arg"]!.Type != JTokenType.Object) return false;
            var arg = Deserialize<CoinTRSocketArgument>(message["arg"]!);
            if (arg == null) return false;
            if (arg.Data == null) return false;
            if (arg.Success == false) return false;

            var argument = (request).Arguments.FirstOrDefault(x => x.InstrumentId == arg.Data.InstrumentId && x.InstrumentType == arg.Data.InstrumentType && x.Channel == arg.Data.Channel);
            if (argument == null) return false;

            if (evt == "subscribe")
            {
                Logger.Log(LogLevel.Trace, $"Socket {connection.Id} Subscription finished");
                return true;
            }

            if (evt == "error")
            {
                var code = message["code"] != null ? message["code"]!.Value<int>() : -1;
                var error = message["msg"] != null ? message["msg"]!.Value<string>() : "Unknown Error!..";
                return true;
            }

            return true;
        }).ConfigureAwait(false);

        return true;
    }
    #endregion

    internal Task<CallResult<WebSocketUpdateSubscription>> SubscribeAsync<T>(IEnumerable<CoinTRSocketArgument> topics, bool authenticated, Action<WebSocketDataEvent<T>> onData, CancellationToken ct)
    {
        var request = new CoinTRSocketRequest
        {
            Operation = "subscribe",
            Arguments = [.. topics],
        };

        return SubscribeAsync(authenticated ? CoinTRAddress.Default.SpotSocketApiPrivateAddress : CoinTRAddress.Default.SpotSocketApiPublicAddress, request, "", authenticated, onData, ct);
    }

    internal async Task<CallResult<bool>> SyncTimeAsync()
    {
        var timeSyncParams = GetTimeSyncInfo();
        if (await timeSyncParams.TimeSyncState.Semaphore.WaitAsync(0).ConfigureAwait(false))
        {
            if (!timeSyncParams.SyncTime || (DateTime.UtcNow - timeSyncParams.TimeSyncState.LastSyncTime < timeSyncParams.RecalculationInterval))
            {
                timeSyncParams.TimeSyncState.Semaphore.Release();
                return new CallResult<bool>(true);
            }

            var sw = Stopwatch.StartNew();
            var localTime = DateTime.UtcNow;
            var result = await GetServerTimestampAsync().ConfigureAwait(false);
            sw.Stop();
            if (!result)
            {
                timeSyncParams.TimeSyncState.Semaphore.Release();
                return result.As(false);
            }

            // Calculate time offset between local and server
            var offset = result.Data - (localTime.AddMilliseconds(sw.ElapsedMilliseconds / 2));
            timeSyncParams.UpdateTimeOffset(offset);
            timeSyncParams.TimeSyncState.Semaphore.Release();
        }

        return new CallResult<bool>(true);
    }

    public async Task UnsubscribeAsync(WebSocketUpdateSubscription subscription, bool force = false, CancellationToken ct = default)
    {
        // Soft Unsubscribe
        var wsc = subscription.GetConnection();
        var wss = subscription.GetSubscription();
        await this.UnsubscribeAsync(wsc, wss).ConfigureAwait(false);

        // Force Unsubscribe
        if (force) await base.UnsubscribeAsync(subscription).ConfigureAwait(false);
    }

    public Task UnsubscribeAsync(int subscriptionId, CancellationToken ct = default) => base.UnsubscribeAsync(subscriptionId);
    public Task UnsubscribeAllAsync(CancellationToken ct = default) => base.UnsubscribeAllAsync();
}