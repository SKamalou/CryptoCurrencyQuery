using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.Common.Interfaces;
public interface ICryptoCurrencyService
{
    Task<IEnumerable<CurrencySymbol>> GetCryptoCurrencySymbolsAsync(CancellationToken cancellationToken);
    Task<Dictionary<CurrencySymbol, Quote>> GetCryptoCurrencyQuotesAsync(CryptoCurrencyQuotesLookupDto quotesLookup, CancellationToken cancellationToken);
}