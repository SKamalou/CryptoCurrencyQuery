namespace WebUI.Models;

public class ApiFailureResult : ApiResult
{
    public ApiFailureResult(int errorCode, string errorMessage)
    : base(false)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}