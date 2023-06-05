namespace WebUI.Models;

internal class ApiSuccessResult<TResult> : ApiResult<TResult>
{
    public ApiSuccessResult(TResult data)
        : base(true, data)
    {
    }
}