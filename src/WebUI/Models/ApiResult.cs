namespace WebUI.Models;

internal abstract class ApiResult<TResult>
{
    public ApiResult(bool success, TResult data)
    {
        Success = success;
        Data = data;
    }

    public bool Success { get; }
    public TResult Data { get; }
}