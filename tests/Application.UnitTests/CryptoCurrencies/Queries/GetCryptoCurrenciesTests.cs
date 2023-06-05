using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCryptoCurrencies;
using CryptoCurrencyQuery.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CryptoCurrencyQuery.Application.UnitTests.CryptoCurrencies.Queries;
internal class GetCryptoCurrenciesTests
{
    [Test]
    public async Task GetCryptoCurrenciesQueryHandler_ReturnCryptoCurrencies()
    {
        //Arrange
        #region Mock ICryptoCurrencyService
        IEnumerable<CurrencySymbol> successResult = new List<CurrencySymbol>
        {
            Constants.BTCSymbol,
            Constants.ETHSymbol,
            Constants.BNBSymbol
        };

        var mockCryptoCurrencyService = new Mock<ICryptoCurrencyService>();
        mockCryptoCurrencyService.Setup(s => s.GetCryptoCurrencySymbolsAsync(new CancellationToken())).Returns(Task.FromResult(successResult));
        #endregion

        var query = new GetCryptoCurrenciesQuery();
        var getCryptoCurrenciesQueryHandler = new GetCryptoCurrenciesQueryHandler(mockCryptoCurrencyService.Object);

        //Act
        var result = await getCryptoCurrenciesQueryHandler.Handle(query, new CancellationToken());

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(3);
    }

    [Test]
    public async Task GetCryptoCurrenciesQueryHandler_ReturnEmptyList()
    {
        //Arrange
        #region Mock ICryptoCurrencyService
        IEnumerable<CurrencySymbol> emptyResult = new List<CurrencySymbol>();

        var mockCryptoCurrencyService = new Mock<ICryptoCurrencyService>();
        mockCryptoCurrencyService.Setup(s => s.GetCryptoCurrencySymbolsAsync(new CancellationToken())).Returns(Task.FromResult(emptyResult));
        #endregion

        var query = new GetCryptoCurrenciesQuery();
        var getCryptoCurrenciesQueryHandler = new GetCryptoCurrenciesQueryHandler(mockCryptoCurrencyService.Object);

        //Act
        var result = await getCryptoCurrenciesQueryHandler.Handle(query, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}