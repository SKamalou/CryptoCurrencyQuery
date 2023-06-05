namespace CryptoCurrencyQuery.Domain.ValueObjects;
public class CurrencySymbol : ValueObject
{
    public string Symbol { get; private set; }

    public CurrencySymbol(string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new Exceptions.ValidationException("Symbol", "Symbol can not be null or empty.");

        if (symbol.Length > 10 || symbol.Length < 2)
            throw new Exceptions.ValidationException("Symbol", "Symbol must be a string with length of 2 to 10 character.");

        if (symbol.Contains(" "))
            throw new Exceptions.ValidationException("Symbol", "Symbol can not contain space.");

        this.Symbol = symbol.ToUpper();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
    }

    public override bool Equals(object? obj)
    {
        if (obj?.GetType() == typeof(string))
            return Symbol == obj?.ToString()?.ToUpper();

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Symbol.GetHashCode();
    }

    public override string ToString()
    {
        return Symbol;
    }
}