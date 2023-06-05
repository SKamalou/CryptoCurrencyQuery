namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public record CryptoCurrencyQuotes
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public Dictionary<string, List<CryptoCurrencyInfo>> Data { get; set; }
}