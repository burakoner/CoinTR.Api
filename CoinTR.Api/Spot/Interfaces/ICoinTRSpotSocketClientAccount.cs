namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Account Subscriptions
/// </summary>
public interface ICoinTRSpotSocketClientAccount
{
    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrdersAsync(string symbol, Action<WebSocketDataEvent<CoinTRSpotStreamOrder>> onMessage, CancellationToken ct = default);

    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrdersAsync(IEnumerable<string> symbols, Action<WebSocketDataEvent<CoinTRSpotStreamOrder>> onMessage, CancellationToken ct = default);

    Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBalancesAsync(Action<WebSocketDataEvent<CoinTRSpotStreamBalance>> onMessage, CancellationToken ct = default);
}