using CryptoCurrencyQuery.Application.Common.Exceptions;
using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
using CryptoCurrencyQuery.Infrastructure.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.Common;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using Polly.Timeout;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.Services;
internal class CryptoCurrencyService : ICryptoCurrencyService
{
    private readonly ICryptoCurrencyClient _cryptoCurrencyClient;

    public CryptoCurrencyService(ICryptoCurrencyClient cryptoCurrencyClient)
    {
        _cryptoCurrencyClient = cryptoCurrencyClient ?? throw new ArgumentNullException(nameof(cryptoCurrencyClient));
    }

    public async Task<IEnumerable<CurrencySymbol>> GetCryptoCurrencySymbolsAsync(CancellationToken cancellationToken)
    {
        var cryptoCurrencies = await GetListAsync(cancellationToken);

        return cryptoCurrencies.Select(cryptoCurrency => new CurrencySymbol(cryptoCurrency.Symbol));
    }

    public async Task<IEnumerable<CryptoCurrencyQuoteDto>> GetCryptoCurrencyQuotesAsync(CryptoCurrencyQuotesLookupDto quotesLookup, CancellationToken cancellationToken)
    {
        var quotes = new List<CryptoCurrencyQuoteDto>();
        foreach (CurrencySymbol symbol in quotesLookup.TargeCurrencySymbols)
        {
            var quote = await GetCryptoCurrencyQuoteAsync(quotesLookup.SourceCryptoCurrencySymbol, symbol, cancellationToken);
            quotes.Add(new CryptoCurrencyQuoteDto(symbol, quote));
        }

        return quotes;
    }

    private async Task<List<CryptoCurrencyInfo>> GetListAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _cryptoCurrencyClient.GetListAsync(cancellationToken);

            if (result.Status.ErrorCode > 0)
                throw new CryptoCurrencyException(result.Status.ErrorCode, result.Status.ErrorMessage);

            return result.Data;
        }
        catch (ApiException ex)
        {
            throw ex.ToCryptoCurrencyException();
        }
        catch (TimeoutRejectedException ex)
        {
            throw ex.ToCryptoCurrencyException();
        }
    }

    private async Task<Quote> GetCryptoCurrencyQuoteAsync(CurrencySymbol sourceCryptoCurrencySymbol, CurrencySymbol targeCurrencySymbol, CancellationToken cancellationToken)
    {
        try
        {
            var parameter = new CryptoCurrencyParameter { SourceSymbol = sourceCryptoCurrencySymbol.Symbol, TargetSymbol = targeCurrencySymbol.Symbol };
            var result = await _cryptoCurrencyClient.GetPriceAsync(parameter, cancellationToken);

            if (result.Status?.ErrorCode > 0)
                throw new CryptoCurrencyException(result.Status.ErrorCode, result.Status.ErrorMessage);

            var priceForSymbol =
                result
                .Data?[sourceCryptoCurrencySymbol.Symbol]
                .FirstOrDefault(currency => sourceCryptoCurrencySymbol.Symbol.Equals(currency.Symbol));

            if (priceForSymbol == null)
                throw new NotFoundException($"Price for convert {sourceCryptoCurrencySymbol.Symbol} to {targeCurrencySymbol.Symbol} was not found!");

            var price = priceForSymbol.Quote?[targeCurrencySymbol.Symbol]?.Price;
            if (!price.HasValue)
            {
                throw new NotFoundException($"Price for convert {sourceCryptoCurrencySymbol.Symbol} to {targeCurrencySymbol.Symbol} was not provided!");
            }

            return new Quote(price.Value);
        }
        catch (ApiException ex)
        {
            throw ex.ToCryptoCurrencyException();
        }
        catch (TimeoutRejectedException ex)
        {
            throw ex.ToCryptoCurrencyException();
        }
    }
}