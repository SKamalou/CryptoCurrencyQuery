using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public record GetCurrentQuotesQuery : IRequest<List<CryptoCurrencyQuoteDto>>
{
    public string? Symbol { get; set; }
}

public class GetCurrentQuotesQueryHandler : IRequestHandler<GetCurrentQuotesQuery, List<CryptoCurrencyQuoteDto>>
{
    public async Task<List<CryptoCurrencyQuoteDto>> Handle(GetCurrentQuotesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new List<CryptoCurrencyQuoteDto>() { new CryptoCurrencyQuoteDto { Symbol = "GBP", Price = 3.71 }, new CryptoCurrencyQuoteDto { Symbol = "USD", Price = 1.34 } });
    }
}