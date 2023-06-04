namespace WebUI.Models;

public abstract class ApiResult
{
    public ApiResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}
