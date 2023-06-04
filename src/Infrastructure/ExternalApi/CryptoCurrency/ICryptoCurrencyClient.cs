using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;

public interface ICryptoCurrencyClient
{
    [Get("/v1/cryptocurrency/listings/latest")]
    Task<CryptoCurrencyList> GetListAsync(CancellationToken cancellationToken);

    [Get("/v2/cryptocurrency/quotes/latest")]
    Task<CryptoCurrencyQuotes> GetPriceAsync(CryptoCurrencyParameter parameter, CancellationToken cancellationToken);
}