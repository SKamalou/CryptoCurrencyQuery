namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public record CryptoCurrencyList
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public List<CryptoCurrencyInfo> Data { get; set; }
}
