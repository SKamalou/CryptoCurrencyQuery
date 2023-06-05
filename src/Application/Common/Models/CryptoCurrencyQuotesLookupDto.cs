using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.Common.Models;
public class CryptoCurrencyQuotesLookupDto
{
    public CurrencySymbol SourceCryptoCurrencySymbol { get; set; }
    public IEnumerable<CurrencySymbol> TargeCurrencySymbols { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var input = (CryptoCurrencyQuotesLookupDto)obj;

        return
            SourceCryptoCurrencySymbol.Equals(input.SourceCryptoCurrencySymbol) &&
            TargeCurrencySymbols.OrderBy(x => x.Symbol).SequenceEqual(input.TargeCurrencySymbols.OrderBy(x => x.Symbol));
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}