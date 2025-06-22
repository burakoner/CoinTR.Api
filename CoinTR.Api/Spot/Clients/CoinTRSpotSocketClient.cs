namespace CoinTR.Api.Spot;

public partial class CoinTRSpotSocketClient : WebSocketApiClient, ICoinTRSpotSocketClient
{
    // Internal
    internal ILogger Logger { get => _logger; }
    internal TimeSyncState TimeSyncState { get; } = new("Binance Spot WS");
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
        callResult = null;

        if (data.Type != JTokenType.Object)
            return false;

        if (data["id"] == null) return false;
        var id = data["id"]!.Value<int>();

        if (data["status"] == null) return false;
        var status = data["status"]!.Value<int>();

        if (request is BinanceSocketQuery query)
        {
            if (query.Id != id) return false;

            if (status != 200)
            {
                var errorCode = data["error"]?["code"]?.Value<int>() ?? status;
                var errorMessage = data["error"]?["msg"]?.Value<string>() ?? "Undefined Error";
                if (status == 418 || status == 429)
                {
                    // Rate limit error 
                    return new CallResult<T>(new BinanceRateLimitError(errorCode, errorMessage, null)
                    {
                        // RetryAfter = data["error"]?["data"].Data.Error.Data!.RetryAfter
                    }, SocketOptions.RawResponse ? data.ToString() : null);
                }

                callResult = new CallResult<T>(new ServerError(errorCode, errorMessage), SocketOptions.RawResponse ? data.ToString() : null);
                return true;
            }

            var error = data["error"];
            if (error != null && error["code"] != null && error["msg"] != null)
            {
                callResult = new CallResult<T>(new ServerError(error["code"]!.Value<int>(), error["msg"]!.ToString()));
                return true;
            }

            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data, SocketOptions.RawResponse ? data.ToString() : null);
            return true;
        }

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
        await Task.CompletedTask;
        return new CallResult<bool>(true);
        throw new NotImplementedException();
    }

    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        var topics = ((BinanceSocketRequest)subscription.Request!).Params;
        var unsub = new BinanceSocketRequest { Method = "UNSUBSCRIBE", Params = topics, Id = NextId() };
        var result = false;

        if (!connection.Connected)
            return true;

        await connection.SendAndWaitAsync(unsub, ClientOptions.ResponseTimeout, data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            var id = data["id"];
            if (id == null)
                return false;

            if ((int)id != unsub.Id)
                return false;

            var result = data["result"];
            if (result?.Type == JTokenType.Null)
            {
                result = true;
                return true;
            }

            return true;
        }).ConfigureAwait(false);
        return result;
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
        if (force)
        {
            await base.UnsubscribeAsync(subscription).ConfigureAwait(false);
        }
    }

    public Task UnsubscribeAsync(int subscriptionId, CancellationToken ct = default)
    {
        return base.UnsubscribeAsync(subscriptionId);
    }

    public Task UnsubscribeAllAsync(CancellationToken ct = default)
    {
        return base.UnsubscribeAllAsync();
    }
}