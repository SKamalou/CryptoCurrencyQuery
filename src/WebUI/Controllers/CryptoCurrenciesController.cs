using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Domain.Enums;
using CryptoCurrencyQuery.Domain.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace CryptoCurrencyQuery.WebUI.Controllers;

public class CryptoCurrenciesController : ApiControllerBase
{
    private const string lastSelectedCryptoCurrencyCacheKey = "LastSelectedCryptoCurrency";
    private const string listOfCryptoCurrenciesCacheKey = "ListOfCryptoCurrenciesCacheKey";

    private readonly IDistributedCache _cache;

    public CryptoCurrenciesController(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    [HttpGet]
    public async Task<ActionResult<List<CurrencySymbol>>> GetAllCryptoCurrencies(CancellationToken cancellationToken)
    {
        Log.Debug("CryptoCurrenciesController: Start the call.");

        List<CurrencySymbol> symbols;

        if (_cache.TryGetValue(listOfCryptoCurrenciesCacheKey, out symbols))
            return Ok(symbols);

        try
        {
            symbols = await Mediator.Send(new GetCryptoCurrenciesQuery());

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

            await _cache.SetAsync(listOfCryptoCurrenciesCacheKey, symbols, cacheEntryOptions);

            return Ok(symbols);
        }
        catch (CryptoCurrencyException ex)
        {
            return new ContentResult { StatusCode = 500, Content = ex.Message };
        }
        catch (OperationCanceledException)
        {
            Log.Debug("CryptoCurrenciesController: The operation was canceled.");

            return NoContent();
        }
    }

    [HttpGet("selected")]
    public async Task<string?> GetLastSelectedCryptoCurrency()
    {
        if (_cache.TryGetValue(lastSelectedCryptoCurrencyCacheKey, out string? symbol))
            return await Task.FromResult(symbol);

        return await Task.FromResult("");
    }

    [HttpGet("{symbol}")]
    public async Task<ActionResult<List<CryptoCurrencyQuoteDto>>> GetCryptoCurrencyDetails(string symbol, CancellationToken cancellationToken)
    {
        await _cache.SetAsync(lastSelectedCryptoCurrencyCacheKey, symbol);

        Log.Debug("CryptoCurrenciesController: Start the call.");

        try
        {
            GetCurrentQuotesQuery command = new GetCurrentQuotesQuery { Symbol = symbol };
            var result = await Mediator.Send(command);
            
            return Ok(result);
        }
        catch (CryptoCurrencyException ex)
        {
            return new ContentResult { StatusCode = 500, Content = ex.Message };
        }
        catch (OperationCanceledException)
        {
            Log.Debug("CryptoCurrenciesController: The operation was canceled.");

            return NoContent();
        }
    }
}