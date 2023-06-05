namespace CryptoCurrencyQuery.Domain.ValueObjects;
public class Quote : ValueObject
{
    public double Price { get; private set; }

    public Quote(double price)
    {
        this.Price = price;
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