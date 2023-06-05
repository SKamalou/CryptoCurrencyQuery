namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public record CryptoCurrencyInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public Dictionary<string, QuoteInfo> Quote { get; set; }
}
