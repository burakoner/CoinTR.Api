namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Symbol
/// </summary>
public record CoinTRSpotSymbol
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = "";

    [JsonProperty("baseCoin")]
    public string BaseAsset { get; set; } = "";

    [JsonProperty("quoteCoin")]
    public string QuoteAsset { get; set; } = "";

    [JsonProperty("minTradeAmount")]
    public string MinimumTradeAmount { get; set; } = "";

    [JsonProperty("maxTradeAmount")]
    public string MaximumTradeAmount { get; set; } = "";

    [JsonProperty("takerFeeRate")]
    public string TakerFeeRate { get; set; } = "";

    [JsonProperty("makerFeeRate")]
    public string MakerFeeRate { get; set; } = "";

    [JsonProperty("pricePrecision")]
    public int PricePrecision { get; set; }

    [JsonProperty("quantityPrecision")]
    public int QuantityPrecision { get; set; }

    [JsonProperty("quotePrecision")]
    public int QuotePrecision { get; set; }

    [JsonProperty("minTradeUSDT")]
    public decimal MinimumTradeInUsdt { get; set; }

    [JsonProperty("status")]
    public CoinTRSpotSymbolStatus Status { get; set; }

    [JsonProperty("buyLimitPriceRatio")]
    public decimal? BuyLimitPriceRatio { get; set; }

    [JsonProperty("sellLimitPriceRatio")]
    public decimal? SellLimitPriceRatio { get; set; }

    [JsonProperty("areaSymbol")]
    public string AreaSymbol { get; set; } = "";
}