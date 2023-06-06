using CryptoCurrencyQuery.Application.Common.Exceptions;
using CryptoCurrencyQuery.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebUI.Models;

namespace CryptoCurrencyQuery.WebUI.Filters;

internal class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleOtherException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails(exception.Errors);

        var content = new ApiFailureResult<ValidationProblemDetails>(details);

        context.Result = new BadRequestObjectResult(content);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        var content = new ApiFailureResult<ValidationProblemDetails>(details);

        context.Result = new BadRequestObjectResult(content);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ProblemDetails()
        {
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        var content = new ApiFailureResult<ProblemDetails>(details);

        context.Result = new NotFoundObjectResult(content);

        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Title = "Forbidden",
        };

        var content = new ApiFailureResult<ProblemDetails>(details);

        context.Result = new ObjectResult(content)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };

        context.ExceptionHandled = true;
    }

    private void HandleOtherException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Title = "InternalServerError",
        };

        var content = new ApiFailureResult<ProblemDetails>(details);

        context.Result = new ObjectResult(content)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}