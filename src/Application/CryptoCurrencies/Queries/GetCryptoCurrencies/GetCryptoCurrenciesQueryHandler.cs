using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
public class GetCryptoCurrenciesQueryHandler : IRequestHandler<GetCryptoCurrenciesQuery, IEnumerable<CurrencySymbol>>
{
    private readonly ICryptoCurrencyService _cryptoCurrencyService;

    public GetCryptoCurrenciesQueryHandler(ICryptoCurrencyService cryptoCurrencyService)
    {
        _cryptoCurrencyService = cryptoCurrencyService ?? throw new ArgumentNullException(nameof(cryptoCurrencyService));
    }

    public async Task<IEnumerable<CurrencySymbol>> Handle(GetCryptoCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await _cryptoCurrencyService.GetCryptoCurrencySymbolsAsync(cancellationToken);
        return result.ToList();
    }
}