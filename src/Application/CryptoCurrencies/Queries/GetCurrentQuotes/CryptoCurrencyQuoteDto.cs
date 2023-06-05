using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public record CryptoCurrencyQuoteDto(CurrencySymbol Symbol, Quote Quote);