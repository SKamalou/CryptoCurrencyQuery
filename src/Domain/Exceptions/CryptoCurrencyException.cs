using System.Runtime.Serialization;

namespace CryptoCurrencyQuery.Domain.Exceptions;
public class CryptoCurrencyException : Exception
{
    public CryptoCurrencyException()
    {
    }

    public CryptoCurrencyException(string message) : base(message)
    {
    }

    public CryptoCurrencyException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CryptoCurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CryptoCurrencyException(int errorCode, string errorMessage) : base($"{errorCode}:'{errorMessage}")
    {
    }
}