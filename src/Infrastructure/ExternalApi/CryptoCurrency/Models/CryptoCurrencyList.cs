namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public class CryptoCurrencyList
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public List<CryptoCurrencyDto> Data { get; set; }
}
