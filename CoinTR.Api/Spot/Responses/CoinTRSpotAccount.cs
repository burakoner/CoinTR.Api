namespace CoinTR.Api.Spot;

public record CoinTRSpotAccount
{
    [JsonProperty("userId")]
    public string UserId { get; set; } = "";

    [JsonProperty("inviterId")]
    public string InviterId { get; set; } = "";

    [JsonProperty("ips")]
    public string IPWhitelist { get; set; } = "";

    [JsonProperty("authorities")]
    public List<string> Authorities { get; set; } = [];

    [JsonProperty("parentId")]
    public long? ParentId { get; set; }

    [JsonProperty("traderType")]
    public string TraderType { get; set; } = "";

    [JsonProperty("channelCode")]
    public string ChannelCode { get; set; } = "";

    [JsonProperty("channel")]
    public string Channel { get; set; } = "";

    [JsonProperty("regisTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RegisterTime { get; set; }
}