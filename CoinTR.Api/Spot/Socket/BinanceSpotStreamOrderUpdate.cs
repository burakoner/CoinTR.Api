﻿namespace CoinTR.Api.Spot;

/// <summary>
/// Update data about an order
/// </summary>
public record BinanceSpotStreamOrderUpdate: BinanceSocketStreamEvent
{
    /// <summary>
    /// The id of the order as assigned by Binance
    /// </summary>
    [JsonProperty("i")]
    public long Id { get; set; }

    /// <summary>
    /// The symbol the order is for
    /// </summary>
    [JsonProperty("s")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// The new client order id
    /// </summary>
    [JsonProperty("c")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// The side of the order
    /// </summary>
    [JsonProperty("S")]
    public CoinTRSpotOrderSide Side { get; set; }

    /// <summary>
    /// The type of the order
    /// </summary>
    [JsonProperty("o")]
    public CoinTRSpotOrderType Type { get; set; }

    /// <summary>
    /// The timespan the order is active
    /// </summary>
    [JsonProperty("f")]
    public CoinTRSpotTimeInForce TimeInForce { get; set; }

    /// <summary>
    /// The quantity of the order
    /// </summary>
    [JsonProperty("q")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The price of the order
    /// </summary>
    [JsonProperty("p")]
    public decimal Price { get; set; }

    /// <summary>
    /// The stop price of the order
    /// </summary>
    [JsonProperty("P")]
    public decimal StopPrice { get; set; }

    /// <summary>
    /// The trailing delta of the order
    /// </summary>
    [JsonProperty("d")]
    public int? TrailingDelta { get; set; }

    /// <summary>
    /// Trailing Time; This is only visible if the trailing stop order has been activated.
    /// </summary>
    [JsonProperty("D")]
    public DateTime TrailingTime { get; set; }

    /// <summary>
    /// The iceberg quantity of the order
    /// </summary>
    [JsonProperty("F")]
    public decimal IcebergQuantity { get; set; }
    /// <summary>
    /// The original client order id
    /// </summary>
    [JsonProperty("C")]
    public string? OriginalClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// The execution type
    /// </summary>
    [JsonProperty("x")]
    public BinanceSpotOrderExecutionType ExecutionType { get; set; }

    /// <summary>
    /// The status of the order
    /// </summary>
    [JsonProperty("X")]
    public CoinTRSpotOrderStatus Status { get; set; }

    /// <summary>
    /// The reason the order was rejected
    /// </summary>
    [JsonProperty("r")]
    public BinanceSpotOrderRejectReason RejectReason { get; set; }

    /// <summary>
    /// The quantity of the last filled trade of this order
    /// </summary>
    [JsonProperty("l")]
    public decimal LastQuantityFilled { get; set; }

    /// <summary>
    /// The quantity of all trades that were filled for this order
    /// </summary>
    [JsonProperty("z")]
    public decimal QuantityFilled { get; set; }

    /// <summary>
    /// The price of the last filled trade
    /// </summary>
    [JsonProperty("L")]
    public decimal LastPriceFilled { get; set; }

    /// <summary>
    /// The fee paid
    /// </summary>
    [JsonProperty("n")]
    public decimal Fee { get; set; }

    /// <summary>
    /// The asset the fee was taken from
    /// </summary>
    [JsonProperty("N")]
    public string FeeAsset { get; set; } = string.Empty;

    /// <summary>
    /// The time of the update
    /// </summary>
    [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// The trade id
    /// </summary>
    [JsonProperty("t")]
    public long TradeId { get; set; }

    /// <summary>
    /// Is working
    /// </summary>
    [JsonProperty("w")]
    public bool IsWorking { get; set; }

    /// <summary>
    /// Whether the buyer is the maker
    /// </summary>
    [JsonProperty("m")]
    public bool BuyerIsMaker { get; set; }

    /// <summary>
    /// Time the order was created
    /// </summary>
    [JsonProperty("O"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Cumulative quantity
    /// </summary>
    [JsonProperty("Z")]
    public decimal QuoteQuantityFilled { get; set; }

    /// <summary>
    /// Quote order quantity
    /// </summary>
    [JsonProperty("Q")]
    public decimal QuoteQuantity { get; set; }

    /// <summary>
    /// Last quote asset transacted quantity (i.e. LastPrice * LastQuantity)
    /// </summary>
    [JsonProperty("Y")]
    public decimal LastQuoteQuantity { get; set; }

    /// <summary>
    /// This id of the corresponding order list. (-1 if not part of an order list)
    /// </summary>
    [JsonProperty("g")]
    public long OrderListId { get; set; }

    // These are unused properties, but are mapped to prevent mapping error of lower/upper case
    /// <summary>
    /// Unused
    /// </summary>
    [JsonProperty("I")]
    public long I { get; set; }

    /// <summary>
    /// Unused
    /// </summary>
    [JsonProperty("M")]
    public bool M { get; set; }

    /// <summary>
    /// Trade group id
    /// </summary>
    [JsonProperty("u")]
    public long? TradeGroupId { get; set; }

    /// <summary>
    /// Prevented match id
    /// </summary>
    [JsonProperty("v")]
    public long? PreventedMatchId { get; set; }

    /// <summary>
    /// Counter order id
    /// </summary>
    [JsonProperty("U")]
    public long? CounterOrderId { get; set; }

    /// <summary>
    /// Prevented quantity
    /// </summary>
    [JsonProperty("A")]
    public decimal? PreventedQuantity { get; set; }

    /// <summary>
    /// Last prevented quantity
    /// </summary>
    [JsonProperty("B")]
    public decimal? LastPreventedQuantity { get; set; }

    /// <summary>
    /// Prevented match id
    /// </summary>
    [JsonProperty("V")]
    [JsonConverter(typeof(MapConverter))]
    public CoinTRSpotSelfTradePreventionMode? SelfTradePreventionMode { get; set; }

    /// <summary>
    /// Working time; when it entered the order book
    /// </summary>
    [JsonProperty("W"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? WorkingTime { get; set; }

    /// <summary>
    /// The listen key the update was for
    /// </summary>
    [JsonIgnore]
    public string ListenKey { get; set; } = string.Empty;
}
