using CryptoCurrencyQuery.Domain.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace CryptoCurrencyQuery.Domain.UnitTests.ValueObjects;
public class QuoteTest
{
    [Test]
    public void ShouldReturnCorrectPrice()
    {
        double price = 1235.12454;

        var quote = new Quote(price);

        quote.Price.Should().Be(price);
    }

    [Test]
    public void ShouldReturnNegativePriceForNullInput()
    {
        var act = () => new Quote(-1);

        act.Should().Throw<ValidationException>();
    }
}