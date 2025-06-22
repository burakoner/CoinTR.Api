namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Order Update
/// </summary>
public record CoinTRSpotStreamOrder
{
    [JsonProperty("instId")]
    public string Symbol { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [JsonProperty("clientOid")]
    public string ClientOrderId { get; set; } = "";

    [JsonProperty("size")]
    public decimal Quantity { get; set; }

    [JsonProperty("newSize")]
    public decimal NewQuantity { get; set; }

    [JsonProperty("notional")]
    public decimal Notional { get; set; }

    [JsonProperty("orderType")]
    public CoinTRSpotOrderType Type { get; set; }

    [JsonProperty("force")]
    public CoinTRSpotTimeInForce TimeInForce { get; set; }

    [JsonProperty("side")]
    public CoinTRSpotOrderSide Side { get; set; }

    [JsonProperty("fillPrice")]
    public decimal? FillPrice { get; set; }

    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    [JsonProperty("baseVolume")]
    public decimal BaseVolume { get; set; }

    [JsonProperty("fillTime")]
    public DateTime? FillTime { get; set; }

    [JsonProperty("fillFee")]
    public decimal? FillFee { get; set; }

    [JsonProperty("fillFeeCoin")]
    public string? FillFeeAsset { get; set; }

    [JsonProperty("tradeScope")]
    public CoinTRSpotTradeScope? TradeScope { get; set; }

    [JsonProperty("accBaseVolume")]
    public decimal? TotalFilledQuantity { get; set; }

    [JsonProperty("priceAvg")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("status")]
    public CoinTRSpotOrderStatus Status { get; set; }

    [JsonProperty("cTime")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime")]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("stpMode")]
    public CoinTRSpotSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    [JsonProperty("enterPointSource")]
    public string EnterPointSource { get; set; } = "";
}
