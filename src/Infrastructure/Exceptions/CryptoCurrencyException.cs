namespace CryptoCurrencyQuery.Infrastructure.Exceptions;
internal class CryptoCurrencyException : Exception
{
    public CryptoCurrencyException(int errorCode, string errorMessage) : this($"{errorCode}: {errorMessage}", null)
    {
    }

    public CryptoCurrencyException(string message, Exception? innerException) : base($"Error in calling extenral API: {message}", innerException)
    {
    }
}