namespace CoinTR.Api.Spot;

/// <summary>
/// Interface for the CoinTR Spot REST API Client
/// </summary>
public interface ICoinTRSpotRestClient : 
    ICoinTRSpotRestClientAccount,
    ICoinTRSpotRestClientPublic,
    ICoinTRSpotRestClientMarket,
    ICoinTRSpotRestClientTrade;
