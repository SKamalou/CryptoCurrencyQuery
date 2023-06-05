using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;

public record GetCryptoCurrenciesQuery : IRequest<IEnumerable<CurrencySymbol>>;