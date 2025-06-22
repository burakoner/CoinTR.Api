namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Order
/// </summary>
public record CoinTRSpotOrder
{
    [JsonProperty("userId")]
    public string UserId { get; set; } = "";

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("orderId")]
    public long OrderId { get; set; }

    [JsonProperty("clientOid")]
    public string ClientOrderId { get; set; } = "";

    [JsonProperty("price")]
    public decimal? Price { get; set; }

    [JsonProperty("size")]
    public decimal Quantity { get; set; }

    [JsonProperty("orderType")]
    public CoinTRSpotOrderType Type { get; set; }

    [JsonProperty("side")]
    public CoinTRSpotOrderSide Side { get; set; }

    [JsonProperty("status")]
    public CoinTRSpotOrderStatus Status { get; set; }

    [JsonProperty("priceAvg")]
    public decimal? AveragePrice { get; set; }

    [JsonProperty("basePrice")]
    public decimal? BasePrice { get; set; }

    [JsonProperty("baseVolume")]
    public decimal BaseVolume { get; set; }

    [JsonProperty("quoteVolume")]
    public decimal QuoteVolume { get; set; }

    [JsonProperty("enterPointSource")]
    public string EnterPointSource { get; set; } = "";

    [JsonProperty("cTime")]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime")]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("baseCoin")]
    public string BaseAsset { get; set; } = "";

    [JsonProperty("quoteCoin")]
    public string QuoteAsset { get; set; } = "";

    [JsonProperty("orderSource")]
    public string OrderSource { get; set; } = "";

    [JsonProperty("feeDetail")]
    public string FeeDetail { get; set; } = "";

    [JsonProperty("tpslType")]
    public string TpSlType { get; set; } = "";

    [JsonProperty("triggerPrice")]
    public decimal? TriggerPrice { get; set; }
}
