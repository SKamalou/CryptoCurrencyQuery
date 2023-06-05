using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Application.UnitTests.CryptoCurrencies;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.Persistence;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CryptoCurrencyQuery.Application.IntegrationTests.CryptoCurrencies.Queries;
internal class GetCurrentQuotesTests
{
    [Test]
    public async Task GetCurrentQuotesQueryHandler_WithInput_ReturnCryptoCurrencyQuotes()
    {
        //Arrange
        #region Mock dbContext
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("TestDB");
        var mockDBContext = new ApplicationDbContext(optionsBuilder.Options, new Infrastructure.Persistence.Interceptors.AuditableEntitySaveChangesInterceptor());
        mockDBContext.PopularCurrencies.AddRange(
            new Domain.Entities.PopularCurrency { Id = 1, Symbol = Constants.USD },
            new Domain.Entities.PopularCurrency { Id = 2, Symbol = Constants.EUR }
            );
        await mockDBContext.SaveChangesAsync();
        #endregion

        #region Mock ICryptoCurrencyService
        var quotesLookup = new CryptoCurrencyQuotesLookupDto
        {
            SourceCryptoCurrencySymbol = Constants.BTCSymbol,
            TargeCurrencySymbols = new List<CurrencySymbol>{
                Constants.USDSymbol,
                Constants.EURSymbol,
            }
        };

        IEnumerable<CryptoCurrencyQuoteDto> successResult = new List<CryptoCurrencyQuoteDto>
        {
            new CryptoCurrencyQuoteDto(Constants.USDSymbol, new Quote(Constants.BTCtoUSD)),
            new CryptoCurrencyQuoteDto(Constants.EURSymbol, new Quote(Constants.BTCtoEUR)),
        };

        var mockCryptoCurrencyService = new Mock<ICryptoCurrencyService>();
        mockCryptoCurrencyService.Setup(mock => mock.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken())).Returns(Task.FromResult(successResult));
        #endregion

        var query = new GetCurrentQuotesQuery(Constants.BTCSymbol);
        var getCryptoCurrenciesQueryHandler = new GetCurrentQuotesQueryHandler(mockDBContext, mockCryptoCurrencyService.Object);

        //Act
        var result = await getCryptoCurrenciesQueryHandler.Handle(query, new CancellationToken());

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(2);
        result.FirstOrDefault(c => c.Symbol.Equals(Constants.USD))?.Quote?.Price.Should().Be(Constants.BTCtoUSD);
        result.FirstOrDefault(c => c.Symbol.Equals(Constants.EUR))?.Quote?.Price.Should().Be(Constants.BTCtoEUR);
    }

    [Test]
    public async Task GetCurrentQuotesQueryHandler_WithoutPopularCurrencies_ReturnEmptyList()
    {
        //Arrange
        #region mock dbContext
        var cancellationToken = new CancellationToken();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("TestDB");
        var mockDBContext = new ApplicationDbContext(optionsBuilder.Options, new Infrastructure.Persistence.Interceptors.AuditableEntitySaveChangesInterceptor());
        #endregion

        var mockCryptoCurrencyService = new Mock<ICryptoCurrencyService>();

        var query = new GetCurrentQuotesQuery(Constants.BTCSymbol);
        var getCryptoCurrenciesQueryHandler = new GetCurrentQuotesQueryHandler(mockDBContext, mockCryptoCurrencyService.Object);

        //Act
        var result = await getCryptoCurrenciesQueryHandler.Handle(query, cancellationToken);

        //Assert
        mockCryptoCurrencyService.Invocations.Should().HaveCount(1);
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public void GetCurrentQuotesQueryValidator_WithInput_MustPass()
    {
        //Arrange
        var query = new GetCurrentQuotesQuery(Constants.BTCSymbol);
        var validator = new GetCurrentQuotesQueryValidator();

        //Act
        var result = validator.Validate(query);

        //Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Test]
    public void GetCurrentQuotesQueryValidator_WithNullInput_MustFail()
    {
        //Arrange
        var query = new GetCurrentQuotesQuery(null);
        var validator = new GetCurrentQuotesQueryValidator();

        //Act
        var result = validator.Validate(query);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().ErrorMessage.Should().Be("Symbol is required.");
    }
}