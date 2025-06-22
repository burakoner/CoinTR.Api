namespace CoinTR.Api.Spot;

internal partial class CoinTRSpotSocketClient
{
    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAggregatedTradesAsync(string symbol, Action<WebSocketDataEvent<BinanceSpotStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        => SubscribeToAggregatedTradesAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAggregatedTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BinanceSpotStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamAggregatedTrade>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Data.Symbol));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@aggTrade").ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(string symbol, Action<WebSocketDataEvent<BinanceSpotStreamTrade>> onMessage, CancellationToken ct = default)
        => SubscribeToTradesAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BinanceSpotStreamTrade>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamTrade>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Data.Symbol));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@trade").ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    // TODO: Kline/Candlestick Streams with timezone offset

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMiniTickersAsync(string symbol, Action<WebSocketDataEvent<BinanceSpotStreamMiniTick>> onMessage, CancellationToken ct = default)
        => SubscribeToMiniTickersAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMiniTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BinanceSpotStreamMiniTick>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamMiniTick>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Data.Symbol));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI  ) + "@miniTicker").ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMiniTickersAsync(Action<WebSocketDataEvent<IEnumerable<BinanceSpotStreamMiniTick>>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<IEnumerable<BinanceSpotStreamMiniTick>>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Stream ?? ""));
        });

        return SubscribeAsync(["!miniTicker@arr"], false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(string symbol, Action<WebSocketDataEvent<BinanceSpotStreamTick>> onMessage, CancellationToken ct = default)
        => SubscribeToTickersAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BinanceSpotStreamTick>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamTick>>>(data =>
        {
            onMessage(data.As<BinanceSpotStreamTick>(data.Data.Data, data.Data.Data.Symbol));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@ticker").ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<WebSocketDataEvent<IEnumerable<BinanceSpotStreamTick>>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<IEnumerable<BinanceSpotStreamTick>>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Stream ?? ""));
        });

        return SubscribeAsync(["!ticker@arr"], false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToRollingWindowTickersAsync(string symbol, TimeSpan windowSize, Action<WebSocketDataEvent<BinanceSpotStreamRollingWindowTick>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamRollingWindowTick>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Stream ?? ""));
        });

        var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
        return SubscribeAsync([$"{symbol.ToLowerInvariant()}@ticker_{windowString}"], false, handler, ct);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToRollingWindowTickersAsync(TimeSpan windowSize, Action<WebSocketDataEvent<IEnumerable<BinanceSpotStreamRollingWindowTick>>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<IEnumerable<BinanceSpotStreamRollingWindowTick>>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Stream ?? ""));
        });

        var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
        return await SubscribeAsync([$"!ticker_{windowString}@arr"], false, handler, ct).ConfigureAwait(false);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBookTickersAsync(string symbol, Action<WebSocketDataEvent<BinanceSpotStreamBookPrice>> onMessage, CancellationToken ct = default)
        => SubscribeToBookTickersAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBookTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<BinanceSpotStreamBookPrice>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamBookPrice>>>(data =>
        {
            onMessage(data.As(data.Data.Data, data.Data.Data.Symbol));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@bookTicker").ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPartialOrderBooksAsync(string symbol, int levels, int? updateInterval, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default)
        => SubscribeToPartialOrderBooksAsync([symbol], levels, updateInterval, onMessage, ct);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPartialOrderBooksAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));
        levels.ValidateIntValues(nameof(levels), 5, 10, 20);

        updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<CoinTRSpotOrderBook>>>(data =>
        {
            onMessage(data.As(data.Data.Data));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@depth" + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
        return await SubscribeAsync(topics, false, handler, ct).ConfigureAwait(false);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(string symbol, int? updateInterval, Action<WebSocketDataEvent<BinanceSpotStreamOrderBook>> onMessage, CancellationToken ct = default)
        => SubscribeToOrderBooksAsync([symbol], updateInterval, onMessage, ct);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(IEnumerable<string> symbols, int? updateInterval, Action<WebSocketDataEvent<BinanceSpotStreamOrderBook>> onMessage, CancellationToken ct = default)
    {
        symbols.ValidateNotNull(nameof(symbols));
        updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

        var handler = new Action<WebSocketDataEvent<BinanceSocketCombinedStream<BinanceSpotStreamOrderBook>>>(data =>
        {
            onMessage(data.As<BinanceSpotStreamOrderBook>(data.Data.Data));
        });

        var topics = symbols.Select(a => a.ToLower(CoinTRConstants.CI) + "@depth" + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
        return await SubscribeAsync(topics, false, handler, ct);
    }
}