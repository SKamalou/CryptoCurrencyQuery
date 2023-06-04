namespace WebUI.Models;

public class ApiSuccessResult<TResult> : ApiResult
{
    public ApiSuccessResult(TResult data)
        : base(true)
    {
        Data = data;
    }

    public TResult Data { get; set; }
}