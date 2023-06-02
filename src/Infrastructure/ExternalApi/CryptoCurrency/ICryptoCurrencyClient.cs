using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;

[Headers("X-CMC_PRO_API_KEY: 86fb6c70-2924-40e4-8565-f92d88583ce1")]
internal interface ICryptoCurrencyClient
{
    [Get("/v1/cryptocurrency/listings/latest")]
    Task<CryptoCurrencyList> GetListAsync(CancellationToken ct);

    [Get("/v2/cryptocurrency/quotes/latest")]
    Task<CryptoCurrencyQuotes> GetPriceAsync(CryptoCurrencyParameter parameter, CancellationToken ct);
}
