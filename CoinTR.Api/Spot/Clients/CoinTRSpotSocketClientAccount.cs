namespace CoinTR.Api.Spot;

internal partial class CoinTRSpotSocketClient
{
    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrdersAsync(string symbol, Action<WebSocketDataEvent<CoinTRSpotStreamOrder>> onMessage, CancellationToken ct = default)
        => SubscribeToOrdersAsync([symbol], onMessage, ct);

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<CoinTRSpotStreamOrder>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<CoinTRSocketResponse<List<CoinTRSpotStreamOrder>>>>(data =>
        {
            foreach (var item in data.Data.Data)
            {
                item.Symbol = data.Data.Argument.InstrumentId!;
                onMessage(data.As(item, item.Symbol));
            }
        });

        var topics = symbols.Select(a => new CoinTRSocketArgument
        {
            InstrumentId = a,
            InstrumentType = "SPOT",
            Channel = $"orders"
        }).ToArray();
        return await SubscribeAsync(topics, true, handler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBalancesAsync(Action<WebSocketDataEvent<CoinTRSpotStreamBalance>> onMessage, CancellationToken ct = default)
    {
        var handler = new Action<WebSocketDataEvent<CoinTRSocketResponse<List<CoinTRSpotStreamBalance>>>>(data =>
        {
            foreach (var item in data.Data.Data)
            {
                onMessage(data.As(item));
            }
        });

        var topic = new CoinTRSocketArgument
        {
            InstrumentType = "SPOT",
            Channel = $"account",
            Asset = "default",
        };
        return await SubscribeAsync([topic], true, handler, ct).ConfigureAwait(false);
    }
}