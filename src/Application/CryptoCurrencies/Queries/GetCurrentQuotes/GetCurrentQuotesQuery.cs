using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public record GetCurrentQuotesQuery(CurrencySymbol Symbol) : IRequest<IEnumerable<CryptoCurrencyQuoteDto>>;