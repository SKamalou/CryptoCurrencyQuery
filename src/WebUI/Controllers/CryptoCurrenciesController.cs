using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.WebUI.Common;
using CryptoCurrencyQuery.WebUI.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebUI.Models;

namespace CryptoCurrencyQuery.WebUI.Controllers;

public class CryptoCurrenciesController : ApiControllerBase
{
    private const string lastSelectedCryptoCurrencyCacheKey = "LastSelectedCryptoCurrency";
    private const string listOfCryptoCurrenciesCacheKey = "ListOfCryptoCurrenciesCacheKey";

    private readonly IDistributedCache _cache;
    private readonly IConfigurationService _configurationService;

    public CryptoCurrenciesController(IDistributedCache cache, IConfigurationService configurationService)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiSuccessResult<List<CurrencySymbol>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiFailureResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CurrencySymbol>>> GetAllCryptoCurrencies(CancellationToken cancellationToken)
    {
        List<CurrencySymbol> symbols;

        if (_cache.TryGetValue(listOfCryptoCurrenciesCacheKey, out symbols))
            return Ok(symbols);

        symbols = (await Mediator.Send(new GetCryptoCurrenciesQuery(), cancellationToken)).ToList();

        var cacheConfig = _configurationService.GetConfig<CachingConfig>();
        var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheConfig.CryptoCurrenciesExpireTimeInMinute));

        await _cache.SetAsync(listOfCryptoCurrenciesCacheKey, symbols, cacheEntryOptions);

        return Ok(symbols);
    }

    [HttpGet("lastSelected")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult<string?> GetLastSelectedCryptoCurrency()
    {
        if (_cache.TryGetValue(lastSelectedCryptoCurrencyCacheKey, out string? symbol))
            return Ok(symbol);

        return Ok("");
    }

    [HttpGet("quotes/{symbol}")]
    [ProducesResponseType(typeof(ApiSuccessResult<List<CryptoCurrencyQuoteDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiFailureResult<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiFailureResult<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiFailureResult<ProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CryptoCurrencyQuoteDto>>> GetCryptoCurrencyDetails(string symbol, CancellationToken cancellationToken)
    {
        var currencySymbol = new CurrencySymbol(symbol);
        var command = new GetCurrentQuotesQuery(currencySymbol);
        var result = await Mediator.Send(command, cancellationToken);

        await _cache.SetAsync(lastSelectedCryptoCurrencyCacheKey, symbol);

        return Ok(result);
    }
}