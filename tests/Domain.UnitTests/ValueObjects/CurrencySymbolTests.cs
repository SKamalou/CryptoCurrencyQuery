using CryptoCurrencyQuery.Domain.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace CryptoCurrencyQuery.Domain.UnitTests.ValueObjects;

public class CurrencySymbolTests
{
    [Test]
    public void CurrencySymbol_ReturnCorrectSymbol()
    {
        //Arrange
        var symbol = "btc";

        //Act
        var currencySymbol = new CurrencySymbol(symbol);

        //Assert
        currencySymbol.Symbol.Should().Be(symbol.ToUpper());
    }

    [Test]
    public void CurrencySymbol_EqualWithCorrectSymbol()
    {
        //Arrange
        var symbol = "btc";

        //Act
        var currencySymbol = new CurrencySymbol(symbol);

        //Assert
        currencySymbol.Equals(symbol).Should().BeTrue();
    }

    [Test]
    public void CurrencySymbol_ReturnSymbolToString()
    {
        //Arrange
        var symbol = "btc";

        //Act
        var currencySymbol = new CurrencySymbol(symbol);

        //Assert
        currencySymbol.ToString().Should().Be(symbol.ToUpper());
    }

    [Test]
    public void CurrencySymbol_WithNullInput_ThrowValidationException()
    {
        //Arrange
        string symbol = null;

        //Act
        var act = () => new CurrencySymbol(symbol);

        //Assert
        act.Should().Throw<ValidationException>();
    }

    [Test]
    public void CurrencySymbol_WithShortInput_ThrowValidationException()
    {
        //Arrange
        var symbol = "A";

        //Act
        var act = () => new CurrencySymbol(symbol);

        //Assert
        act.Should().Throw<ValidationException>();
    }

    [Test]
    public void CurrencySymbol_WithLongInput_ThrowValidationException()
    {
        //Arrange
        var symbol = "ABCDEFGHIJK";

        //Act
        var act = () => new CurrencySymbol(symbol);

        //Assert
        act.Should().Throw<ValidationException>();
    }

    [Test]
    public void CurrencySymbol_WithInputContainsSpace_ThrowValidationException()
    {
        //Arrange
        var symbol = "A B";

        //Act
        var act = () => new CurrencySymbol(symbol);

        //Assert
        act.Should().Throw<ValidationException>();
    }
}