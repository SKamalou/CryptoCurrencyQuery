namespace CryptoCurrencyQuery.Domain.ValueObjects;
public class CurrencySymbol : ValueObject
{
    public string Symbol { get; private set; }

    public CurrencySymbol(string symbol)
    {
        this.Symbol = symbol.ToUpper();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
    }

    public override bool Equals(object obj)
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