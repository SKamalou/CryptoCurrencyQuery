using CryptoCurrencyQuery.Domain.Exceptions;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.Common;
internal static class ApiExceptionExtensions
{
    public static CryptoCurrencyException ToCryptoCurrencyException(this ApiException exception)
    {
        var message = $"{exception.Message} @{exception.HttpMethod.Method}('{exception.Uri?.AbsoluteUri}') Content: '{exception.Content}'";
        return new CryptoCurrencyException(message, exception);
    }
}