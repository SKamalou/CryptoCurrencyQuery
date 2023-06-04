using CryptoCurrencyQuery.Domain.Exceptions;

namespace CryptoCurrencyQuery.Domain.ValueObjects;
public class Quote : ValueObject
{
    public double Price { get; private set; }

    public Quote(double? price)
    {
        if (!price.HasValue)
            throw new CryptoCurrencyException("Incorrect api result!", null);

        this.Price = price.Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
    }

    public override string ToString()
    {
        return Price.ToString();
    }
}