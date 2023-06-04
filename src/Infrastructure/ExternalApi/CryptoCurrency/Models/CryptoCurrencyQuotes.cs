namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public class CryptoCurrencyQuotes
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public Dictionary<string, List<CryptoCurrencyDto>> Data { get; set; }
}
