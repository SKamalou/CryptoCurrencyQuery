namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public class CryptoCurrencyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public Dictionary<string, QuoteInfo> Quote { get; set; }
}
