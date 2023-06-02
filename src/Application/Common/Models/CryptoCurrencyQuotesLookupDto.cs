using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.Common.Models;
public class CryptoCurrencyQuotesLookupDto
{
    public CurrencySymbol SourceCryptoCurrencySymbol { get; set; }
    public IEnumerable<CurrencySymbol> TargeCurrencySymbols { get; set; }
}