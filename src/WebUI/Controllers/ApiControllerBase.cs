using CryptoCurrencyQuery.WebUI.Filters;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebUI.Models;

namespace CryptoCurrencyQuery.WebUI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public OkObjectResult Ok<TResult>([ActionResultObjectValue] TResult value)
    {
        var apiResult = new ApiSuccessResult<TResult>(value);
        return base.Ok(apiResult);
    }
}