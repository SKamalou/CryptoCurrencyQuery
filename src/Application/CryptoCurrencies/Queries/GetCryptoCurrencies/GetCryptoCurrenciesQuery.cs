using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
public record GetCryptoCurrenciesQuery : IRequest<List<CryptoCurrencyBriedDto>>;

public class GetCryptoCurrenciesQueryHandler : IRequestHandler<GetCryptoCurrenciesQuery, List<CryptoCurrencyBriedDto>>
{
    public async Task<List<CryptoCurrencyBriedDto>> Handle(GetCryptoCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new List<CryptoCurrencyBriedDto>() { new CryptoCurrencyBriedDto { Symbol = "BTC" }, new CryptoCurrencyBriedDto { Symbol = "GBP" }, new CryptoCurrencyBriedDto { Symbol = "EUR" } });
    }
}