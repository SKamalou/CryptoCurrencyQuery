using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.Common.Interfaces;
public interface ICryptoCurrencyService
{
    Task<IEnumerable<CurrencySymbol>> GetCryptoCurrencySymbolsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CryptoCurrencyQuoteDto>> GetCryptoCurrencyQuotesAsync(CryptoCurrencyQuotesLookupDto quotesLookup, CancellationToken cancellationToken);
}