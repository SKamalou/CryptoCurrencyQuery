namespace CryptoCurrencyQuery.Domain.Exceptions;
public class InvalidSymbolException : Exception
{
    public InvalidSymbolException() : base("Symbol must be a string with length of 3 to 10 character.")
    {
    }
}