namespace CoinTR.Api.Spot;

public partial class CoinTRSpotSocketClient
{
    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(string symbol, Action<WebSocketDataEvent<CoinTRSpotStreamTicker>> onMessage, CancellationToken ct = default)
        => SubscribeToTickersAsync([symbol], onMessage, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<CoinTRSpotStreamTicker>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<CoinTRSocketResponse<List<CoinTRSpotStreamTicker>>>>(data =>
        {
            foreach (var item in data.Data.Data)
            {
                onMessage(data.As(item, item.Symbol));
            }
        });

        var topics = symbols.Select(a => new CoinTRSocketArgument
        {
            InstrumentId = a,
            InstrumentType = "SPOT",
            Channel = "ticker"
        }).ToArray();
        return SubscribeAsync(topics, false, handler, ct);
    }

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(string symbol, int levels, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default)
        => SubscribeToOrderBooksAsync([symbol], levels, onMessage, ct);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(IEnumerable<string> symbols, int levels, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default)
    {
        levels.ValidateIntValues(nameof(levels), 1, 5, 15);

        var handler = new Action<WebSocketDataEvent<CoinTRSocketResponse<List<CoinTRSpotOrderBook>>>>(data =>
        {
            foreach (var item in data.Data.Data)
            {
                item.Symbol = data.Data.Argument.InstrumentId;
                onMessage(data.As(item, item.Symbol));
            }
        });

        var topics = symbols.Select(a => new CoinTRSocketArgument
        {
            InstrumentId = a,
            InstrumentType = "SPOT",
            Channel = $"books{levels}"
        }).ToArray();
        return await SubscribeAsync(topics, false, handler, ct).ConfigureAwait(false);
    }
}