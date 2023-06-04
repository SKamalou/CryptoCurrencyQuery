using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;

public interface ICryptoCurrencyClient
{
    [Get("/v1/cryptocurrency/listings/latest")]
    Task<CryptoCurrencyList> GetListAsync(CancellationToken ct);

    [Get("/v2/cryptocurrency/quotes/latest")]
    Task<CryptoCurrencyQuotes> GetPriceAsync(CryptoCurrencyParameter parameter, CancellationToken ct);
}