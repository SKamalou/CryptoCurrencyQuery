namespace CryptoCurrencyQuery.Domain.Exceptions;
public class CryptoCurrencyException : Exception
{
    public CryptoCurrencyException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public CryptoCurrencyException(int errorCode, string errorMessage) : base($"{errorCode}:'{errorMessage}")
    {
    }
}