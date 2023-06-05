using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.ValueObjects;
using MediatR;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public class GetCurrentQuotesQueryHandler : IRequestHandler<GetCurrentQuotesQuery, IEnumerable<CryptoCurrencyQuoteDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICryptoCurrencyService _cryptoCurrencyService;

    public GetCurrentQuotesQueryHandler(IApplicationDbContext context, ICryptoCurrencyService cryptoCurrencyService)
    {
        this._context = context;
        _cryptoCurrencyService = cryptoCurrencyService;
    }

    public async Task<IEnumerable<CryptoCurrencyQuoteDto>> Handle(GetCurrentQuotesQuery request, CancellationToken cancellationToken)
    {
        var targetCurrencySymbols = _context.PopularCurrencies.Select(currency => new CurrencySymbol(currency.Symbol)).ToList();

        if (!targetCurrencySymbols.Any())
            return new List<CryptoCurrencyQuoteDto>();

        var getCryptoCurrencyQuotesQuery = new CryptoCurrencyQuotesLookupDto(request.Symbol, targetCurrencySymbols);

        var result = await _cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(getCryptoCurrencyQuotesQuery, cancellationToken);

        return result;
    }
}