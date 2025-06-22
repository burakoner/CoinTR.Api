namespace CoinTR.Api.Spot;

/// <summary>
/// Interface for the CoinTR Spot Web Socket API Client Public Stream Methods
/// </summary>
public interface ICoinTRSpotSocketClientPublic
{
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(string symbol, Action<WebSocketDataEvent<CoinTRSpotStreamTicker>> onMessage, CancellationToken ct = default);

    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<CoinTRSpotStreamTicker>> onMessage, CancellationToken ct = default);

    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(string symbol, int levels, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default);

    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBooksAsync(IEnumerable<string> symbols, int levels, Action<WebSocketDataEvent<CoinTRSpotOrderBook>> onMessage, CancellationToken ct = default);
}