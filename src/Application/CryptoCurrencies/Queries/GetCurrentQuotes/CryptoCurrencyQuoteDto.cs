namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public class CryptoCurrencyQuoteDto
{
    public string? Symbol { get; set; }
    public double Price { get; set; }
}