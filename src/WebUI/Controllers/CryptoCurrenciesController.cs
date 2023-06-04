using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.Common;
using CryptoCurrencyQuery.Infrastructure.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace CryptoCurrencyQuery.WebUI.Controllers;

public class CryptoCurrenciesController : ApiControllerBase
{
    private const string lastSelectedCryptoCurrencyCacheKey = "LastSelectedCryptoCurrency";
    private const string listOfCryptoCurrenciesCacheKey = "ListOfCryptoCurrenciesCacheKey";

    private readonly IDistributedCache _cache;
    private readonly IConfiguration _configuration;

    public CryptoCurrenciesController(IDistributedCache cache,IConfiguration configuration)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    [HttpGet]
    public async Task<ActionResult<List<CurrencySymbol>>> GetAllCryptoCurrencies(CancellationToken cancellationToken)
    {
        List<CurrencySymbol> symbols;

        if (_cache.TryGetValue(listOfCryptoCurrenciesCacheKey, out symbols))
            return Ok(symbols);

        symbols = await Mediator.Send(new GetCryptoCurrenciesQuery(), cancellationToken);

        var cacheConfig = _configuration.BindTo<CachingConfig>();
        var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheConfig.CryptoCurrenciesExpireTimeInMinute));

        await _cache.SetAsync(listOfCryptoCurrenciesCacheKey, symbols, cacheEntryOptions);

        return Ok(symbols);
    }

    [HttpGet("selected")]
    public async Task<ActionResult<string?>> GetLastSelectedCryptoCurrency()
    {
        if (_cache.TryGetValue(lastSelectedCryptoCurrencyCacheKey, out string? symbol))
            return Ok(symbol);

        return Ok("");
    }

    [HttpGet("quotes/{symbol}")]
    public async Task<ActionResult<List<CryptoCurrencyQuoteDto>>> GetCryptoCurrencyDetails(string symbol, CancellationToken cancellationToken)
    {
        await _cache.SetAsync(lastSelectedCryptoCurrencyCacheKey, symbol);

        var command = new GetCurrentQuotesQuery { Symbol = symbol };
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}