using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CryptoCurrencyQuery.WebUI.Controllers;

[Authorize]
public class CryptoCurrenciesController : ApiControllerBase
{
    //[HttpGet]
    //public async Task<ActionResult<List<CryptoCurrencyBriedDto>>> Get()
    //{
    //    return await Mediator.Send(new GetCryptoCurrenciesQuery());
    //}

    [HttpGet("{symbol}")]
    [ActionName(nameof(GetCurrentQuotes))]
    public async Task<ActionResult<List<CryptoCurrencyQuoteDto>>> GetCurrentQuotes(string symbol)
    {
        return await Mediator.Send(new GetCurrentQuotesQuery { Symbol=symbol});
    }
}