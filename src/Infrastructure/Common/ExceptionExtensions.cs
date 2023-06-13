using CryptoCurrencyQuery.Infrastructure.Exceptions;
using Polly.Timeout;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.Common;
internal static class ExceptionExtensions
{
    public static CryptoCurrencyException ToCryptoCurrencyException(this ApiException exception)
    {
        var message = $"{exception.Message} @{exception.HttpMethod.Method}('{exception.Uri?.AbsoluteUri}') Content: '{exception.Content}'";
        return new CryptoCurrencyException(message, exception);
    }

    public static CryptoCurrencyException ToCryptoCurrencyException(this TimeoutRejectedException exception)
    {
        return new CryptoCurrencyException(exception.Message, exception);
    }
}