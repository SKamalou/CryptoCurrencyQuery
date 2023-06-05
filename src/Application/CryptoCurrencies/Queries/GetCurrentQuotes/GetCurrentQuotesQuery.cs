using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public record GetCurrentQuotesQuery(CurrencySymbol Symbol) : IRequest<IEnumerable<CryptoCurrencyQuoteDto>>;