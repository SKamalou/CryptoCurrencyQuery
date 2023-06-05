using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using CryptoCurrencyQuery.Infrastructure.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Polly.Timeout;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.UnitTests.Services;
internal class CryptoCurrencyServiceTests
{
    [Test]
    public async Task GetCryptoCurrencySymbolsAsync_ReturnCryptoCurrencies()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var successResult = new CryptoCurrencyList
        {
            Status = new CryptoCurrencyResponseStatus { ErrorCode = 0, ErrorMessage = null },
            Data = new List<CryptoCurrencyInfo> {
                new CryptoCurrencyInfo { Id = 1, Symbol= Constants.BTC,Name= "Bitcoin" },
                new CryptoCurrencyInfo { Id = 1027, Symbol= Constants.ETH,Name= "Ethereum" },
                new CryptoCurrencyInfo { Id = 825, Symbol= Constants.BNB,Name= "Binance" }
            }
        };

        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();
        mockCryptoCurrencyClient.Setup(mock => mock.GetListAsync(new CancellationToken())).Returns(Task.FromResult(successResult));
        #endregion

        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var result = await cryptoCurrencyService.GetCryptoCurrencySymbolsAsync(new CancellationToken());

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(3);
        result.Contains(Constants.BTCSymbol).Should().BeTrue();
        result.Contains(Constants.ETHSymbol).Should().BeTrue();
        result.Contains(Constants.BNBSymbol).Should().BeTrue();
    }

    [Test]
    public async Task GetCryptoCurrencySymbolsAsync_WithErrorStatus_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var errorStatusResult = new CryptoCurrencyList
        {
            Status = new CryptoCurrencyResponseStatus { ErrorCode = 1000, ErrorMessage = "ApiCallFailed!" }
        };

        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();
        mockCryptoCurrencyClient.Setup(mock => mock.GetListAsync(new CancellationToken())).Returns(Task.FromResult(errorStatusResult));
        #endregion

        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencySymbolsAsync(new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    [Test]
    public async Task GetCryptoCurrencySymbolsAsync_WithTimeoutRejectedException_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();
        mockCryptoCurrencyClient.Setup(mock => mock.GetListAsync(new CancellationToken())).Throws<TimeoutRejectedException>();
        #endregion

        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencySymbolsAsync(new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    [Test]
    public async Task GetCryptoCurrencySymbolsAsync_WithApiException_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var apiException = await ApiException.Create(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(), new RefitSettings());
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();
        mockCryptoCurrencyClient.Setup(mock => mock.GetListAsync(new CancellationToken())).Throws(apiException);
        #endregion

        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencySymbolsAsync(new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    [Test]
    public async Task GetCryptoCurrencyQuotesAsync_ReturnQuotes()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();

        var eurParameter = new CryptoCurrencyParameter { SourceSymbol = Constants.BTC, TargetSymbol= Constants.EUR};
        var eurSuccessResult = GetQuoteSuccessResult(Constants.EUR, Constants.BTCtoEUR);
        mockCryptoCurrencyClient.Setup(mock => mock.GetPriceAsync(eurParameter, new CancellationToken())).Returns(eurSuccessResult);

        var usdParameter = new CryptoCurrencyParameter { SourceSymbol = Constants.BTC, TargetSymbol = Constants.USD };
        var usdSuccessResult = GetQuoteSuccessResult(Constants.USD, Constants.BTCtoUSD);
        mockCryptoCurrencyClient.Setup(mock => mock.GetPriceAsync(usdParameter, new CancellationToken())).Returns(usdSuccessResult);
        #endregion

        var quotesLookup = new CryptoCurrencyQuotesLookupDto
        (
            SourceCryptoCurrencySymbol: Constants.BTCSymbol,
            TargeCurrencySymbols: new List<CurrencySymbol>{
                Constants.USDSymbol,
                Constants.EURSymbol,
            }
        );
        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var result = await cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken());

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(2);
        result.First(c => c.Symbol == Constants.EURSymbol).Quote.Price.Should().Be(Constants.BTCtoEUR);
        result.First(c => c.Symbol == Constants.USDSymbol).Quote.Price.Should().Be(Constants.BTCtoUSD);
    }

    [Test]
    public async Task GetCryptoCurrencyQuotesAsync_WithEmptyInput_ReturnEmptyList()
    {
        //Arrange
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();

        var quotesLookup = new CryptoCurrencyQuotesLookupDto(Constants.BTCSymbol, new List<CurrencySymbol>());
        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var result = await cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken());

        //Assert
        mockCryptoCurrencyClient.Invocations.Should().HaveCount(0);
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetCryptoCurrencyQuotesAsync_WithErrorStatus_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var errorStatusResult = new CryptoCurrencyQuotes
        {
            Status = new CryptoCurrencyResponseStatus { ErrorCode = 1000, ErrorMessage = "ApiCallFailed!" }
        };

        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();

        mockCryptoCurrencyClient.Setup(mock => mock.GetPriceAsync(It.IsAny<CryptoCurrencyParameter>(), new CancellationToken())).Returns(Task.FromResult(errorStatusResult));
        #endregion

        var quotesLookup = new CryptoCurrencyQuotesLookupDto
        (
            SourceCryptoCurrencySymbol: Constants.BTCSymbol,
            TargeCurrencySymbols: new List<CurrencySymbol> { Constants.USDSymbol }
        );
        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    [Test]
    public async Task GetCryptoCurrencyQuotesAsync_WithTimeoutRejectedException_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();

        mockCryptoCurrencyClient.Setup(mock => mock.GetPriceAsync(It.IsAny<CryptoCurrencyParameter>(), new CancellationToken())).Throws<TimeoutRejectedException>();
        #endregion

        var quotesLookup = new CryptoCurrencyQuotesLookupDto
        (
            SourceCryptoCurrencySymbol: Constants.BTCSymbol,
            TargeCurrencySymbols: new List<CurrencySymbol> { Constants.USDSymbol }
        );
        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    [Test]
    public async Task GetCryptoCurrencyQuotesAsync_WithApiException_ThrowException()
    {
        //Arrange
        #region Mock ICryptoCurrencyClient
        var apiException = await ApiException.Create(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(), new RefitSettings());
        var mockCryptoCurrencyClient = new Mock<ICryptoCurrencyClient>();

        mockCryptoCurrencyClient.Setup(mock => mock.GetPriceAsync(It.IsAny<CryptoCurrencyParameter>(), new CancellationToken())).Throws(apiException);
        #endregion

        var quotesLookup = new CryptoCurrencyQuotesLookupDto
        (
            SourceCryptoCurrencySymbol: Constants.BTCSymbol,
            TargeCurrencySymbols: new List<CurrencySymbol> { Constants.USDSymbol }
        );
        var cryptoCurrencyService = new CryptoCurrencyService(mockCryptoCurrencyClient.Object);

        //Act
        var act = () => cryptoCurrencyService.GetCryptoCurrencyQuotesAsync(quotesLookup, new CancellationToken());

        //Assert
        await act.Should().ThrowAsync<CryptoCurrencyException>();
    }

    private Task<CryptoCurrencyQuotes> GetQuoteSuccessResult(string currency, double price)
    {
        var result = new CryptoCurrencyQuotes
        {
            Status = new CryptoCurrencyResponseStatus { ErrorCode = 0, ErrorMessage = null },
            Data = new Dictionary<string, List<CryptoCurrencyInfo>>
            {
                {
                    Constants.BTC,
                    new List<CryptoCurrencyInfo>
                    {
                        new CryptoCurrencyInfo{
                            Id = 1,
                            Symbol = Constants.BTC,
                            Name = "Bitcoin",
                            Quote = new Dictionary<string, QuoteInfo> {{currency, new QuoteInfo{Price=price} }}
                        }
                    }
                }
            }
        };

        return Task.FromResult(result);
    }

    private bool IsEqual(CryptoCurrencyParameter param1, CryptoCurrencyParameter param2) =>
        param1.SourceSymbol == param2.SourceSymbol && param1.TargetSymbol == param2.TargetSymbol;
}