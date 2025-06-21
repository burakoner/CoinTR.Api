﻿using CoinTR.Api.Spot;

namespace CoinTR.Api.Shared;

/// <summary>
/// Helper methods for the Binance API
/// </summary>
public static class BinanceHelpers
{
    /// <summary>
    /// Get the used weight from the response headers
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static int? UsedWeight(this IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        if (headers == null)
            return null;

        var headerValues = headers.SingleOrDefault(s => s.Key.StartsWith("X-MBX-USED-WEIGHT-", StringComparison.InvariantCultureIgnoreCase)).Value;
        if (headerValues != null && int.TryParse(headerValues.First(), out var value))
            return value;
        return null;
    }

    /// <summary>
    /// Get the used weight from the response headers
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static int? UsedOrderCount(this IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        if (headers == null)
            return null;

        var headerValues = headers.SingleOrDefault(s => s.Key.StartsWith("X-MBX-ORDER-COUNT-", StringComparison.InvariantCultureIgnoreCase)).Value;
        if (headerValues != null && int.TryParse(headerValues.First(), out var value))
            return value;
        return null;
    }

    /// <summary>
    /// Clamp a quantity between a min and max quantity and floor to the closest step
    /// </summary>
    /// <param name="minQuantity"></param>
    /// <param name="maxQuantity"></param>
    /// <param name="stepSize"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
    {
        quantity = Math.Min(maxQuantity, quantity);
        quantity = Math.Max(minQuantity, quantity);
        if (stepSize == 0)
            return quantity;
        quantity -= quantity % stepSize;
        quantity = Floor(quantity);
        return quantity;
    }

    /// <summary>
    /// Clamp a price between a min and max price
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal price)
    {
        price = Math.Min(maxPrice, price);
        price = Math.Max(minPrice, price);
        return price;
    }

    /// <summary>
    /// Floor a price to the closest tick
    /// </summary>
    /// <param name="tickSize"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal FloorPrice(decimal tickSize, decimal price)
    {
        price -= price % tickSize;
        price = Floor(price);
        return price;
    }

    /// <summary>
    /// Floor
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static decimal Floor(decimal number)
    {
        return Math.Floor(number * 100000000) / 100000000;
    }

    /// <summary>
    /// Client order id separator
    /// </summary>
    public const string ClientOrderIdSeparator = "-";

    /// <summary>
    /// Apply broker id to a client order id
    /// </summary>
    /// <param name="clientOrderId"></param>
    /// <param name="brokerId"></param>
    /// <param name="maxLength"></param>
    /// <param name="allowValueAdjustment"></param>
    /// <returns></returns>
    public static string ApplyBrokerId(string? clientOrderId, string brokerId, int maxLength, bool allowValueAdjustment)
    {
        var reservedLength = brokerId.Length + ClientOrderIdSeparator.Length;

        if ((clientOrderId?.Length + reservedLength) > maxLength)
            return clientOrderId!;

        if (!string.IsNullOrEmpty(clientOrderId))
        {
            if (allowValueAdjustment && !clientOrderId!.StartsWith(brokerId + ClientOrderIdSeparator))
                clientOrderId = brokerId + ClientOrderIdSeparator + clientOrderId;

            return clientOrderId!;
        }
        else
        {
            // if (string.IsNullOrEmpty(clientOrderId) || !clientOrderId!.StartsWith(brokerId + ClientOrderIdSeparator))
                clientOrderId = ExchangeHelpers.AppendRandomString(brokerId + ClientOrderIdSeparator, maxLength);
        }

        return clientOrderId;
    }
}