namespace CoinTR.Api.Spot;

/// <summary>
/// CoinTR Spot Asset
/// </summary>
public record CoinTRSpotAsset
{
    /// <summary>
    /// Asset ID
    /// </summary>
    [JsonProperty("coinId")]
    public int AssetId { get; set; }

    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("coin")]
    public string Asset { get; set; } = "";

    /// <summary>
    /// Transferable asset
    /// </summary>
    public bool Transfer { get; set; }

    /// <summary>
    /// Chains of the asset
    /// </summary>
    public List<CoinTRSpotAssetChain> Chains { get; set; } = [];
}

/// <summary>
/// CoinTR Spot Asset Chain
/// </summary>
public record CoinTRSpotAssetChain
{
    /// <summary>
    /// Chain
    /// </summary>
    public string Chain { get; set; } = "";

    /// <summary>
    /// Need Tag
    /// </summary>
    public bool NeedTag { get; set; }

    /// <summary>
    /// Withdrawable
    /// </summary>
    public bool Withdrawable { get; set; }

    /// <summary>
    /// Rechargeable
    /// </summary>
    public bool Rechargeable { get; set; }

    /// <summary>
    /// Withdrawal Fee
    /// </summary>
    [JsonProperty("withdrawFee")]
    public decimal WithdrawalFee { get; set; }

    /// <summary>
    /// Extra Withdrawal Fee
    /// </summary>
    [JsonProperty("extraWithdrawFee")]
    public decimal ExtraWithdrawalFee { get; set; }

    /// <summary>
    /// Deposit Confirmation Count
    /// </summary>
    public int DepositConfirm { get; set; }

    /// <summary>
    /// Withdrawal Confirmation Count
    /// </summary>
    [JsonProperty("withdrawConfirm")]
    public int WithdrawalConfirm { get; set; }

    /// <summary>
    /// Minimum Deposit Amount
    /// </summary>
    [JsonProperty("minDepositAmount")]
    public decimal MinimumDepositAmount { get; set; }

    /// <summary>
    /// Minimum Withdrawal Amount
    /// </summary>
    [JsonProperty("minWithdrawAmount")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// Browser URL
    /// </summary>
    public string BrowserUrl { get; set; } = "";

    /// <summary>
    /// Contract Address
    /// </summary>
    public string ContractAddress { get; set; } = "";

    /// <summary>
    /// Withdrawal Step
    /// </summary>
    [JsonProperty("withdrawStep")]
    public int WithdrawalStep { get; set; }

    /// <summary>
    /// Withdrawal Minimum Scale
    /// </summary>
    [JsonProperty("withdrawMinScale")]
    public int WithdrawalMinimumScale { get; set; }
}