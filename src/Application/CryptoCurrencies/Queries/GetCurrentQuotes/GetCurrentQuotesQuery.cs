using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public record GetCurrentQuotesQuery : IRequest<List<CryptoCurrencyQuoteDto>>
{
    public string? Symbol { get; set; }
}

public class GetCurrentQuotesQueryHandler : IRequestHandler<GetCurrentQuotesQuery, List<CryptoCurrencyQuoteDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICryptoCurrencyService _cryptoCurrencyService;

    public GetCurrentQuotesQueryHandler(IApplicationDbContext context, ICryptoCurrencyService cryptoCurrencyService)
    {
        this._context = context;
        _cryptoCurrencyService = cryptoCurrencyService;
    }

    public async Task<List<CryptoCurrencyQuoteDto>> Handle(GetCurrentQuotesQuery request, CancellationToken cancellationToken)
    {
        var targetCurrencySymbols = _context.PopularCurrencies.Select(currency => new CurrencySymbol(currency.Symbol));

        if (!targetCurrencySymbols.Any())
            return new List<CryptoCurrencyQuoteDto>();

        var getCryptoCurrencyQuotesQuery = new CryptoCurrencyQuotesLookupDto
        {
            SourceCryptoCurrencySymbol = new CurrencySymbol(request.Symbol),
            TargeCurrencySymbols = targetCurrencySymbols
        };

        var cryptoCurrencyQuotes = await _cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(getCryptoCurrencyQuotesQuery, cancellationToken);
        var result = cryptoCurrencyQuotes.Select(currency => new CryptoCurrencyQuoteDto(currency.Key.Symbol, currency.Value.Price)).ToList();

        return result;
    }
}