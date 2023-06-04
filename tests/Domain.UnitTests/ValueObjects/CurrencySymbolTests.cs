using CryptoCurrencyQuery.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace CryptoCurrencyQuery.Domain.UnitTests.ValueObjects;

public class CurrencySymbolTests
{
    [Test]
    public void ShouldReturnCorrectSymbol()
    {
        string symbol = "btc";

        var currencySymbol = new CurrencySymbol(symbol);

        currencySymbol.Symbol.Should().Be(symbol.ToUpper());
    }

    [Test]
    public void ShouldEqualWithCorrectSymbol()
    {
        string symbol = "btc";

        var currencySymbol = new CurrencySymbol(symbol);

        currencySymbol.Equals(symbol).Should().BeTrue();
    }

    [Test]
    public void ShouldReturnSymbolInToString()
    {
        string symbol = "btc";

        var currencySymbol = new CurrencySymbol(symbol);

        currencySymbol.ToString().Should().Be(symbol.ToUpper());
    }
}