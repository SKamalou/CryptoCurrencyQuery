namespace WebUI.Models;

public class ApiSuccessResult<TResult> : ApiResult<TResult>
{
    public ApiSuccessResult(TResult data)
        : base(true, data)
    {
    }
}