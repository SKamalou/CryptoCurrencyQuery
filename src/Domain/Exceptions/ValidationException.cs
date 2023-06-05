namespace CryptoCurrencyQuery.Domain.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string source, string message)
        : this()
    {
        this.Errors.Add(source, new string[] { message });
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : this()
    {
        this.Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}