using Microsoft.AspNetCore.Mvc;

namespace WebUI.Models;

internal class ApiFailureResult<TFailureDetails> : ApiResult<TFailureDetails> where TFailureDetails : ProblemDetails
{
    public ApiFailureResult(TFailureDetails data)
        : base(false, data)
    {
    }
}