using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.Common.Models;
using CryptoCurrencyQuery.Domain.Exceptions;
using CryptoCurrencyQuery.Domain.ValueObjects;
using CryptoCurrencyQuery.Infrastructure.Common;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
using Polly.Timeout;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.Services;
public class CryptoCurrencyService : ICryptoCurrencyService
{
    private readonly ICryptoCurrencyClient _cryptoCurrencyClient;

    public CryptoCurrencyService(ICryptoCurrencyClient cryptoCurrencyClient) => _cryptoCurrencyClient = cryptoCurrencyClient;

    public async Task<IEnumerable<CurrencySymbol>> GetCryptoCurrencySymbolsAsync(CancellationToken cancellationToken)
    {
        var cryptoCurrencies = await GetListAsync(cancellationToken);

        return cryptoCurrencies.Select(cryptoCurrency => new CurrencySymbol(cryptoCurrency.Symbol));
    }

    public async Task<Dictionary<CurrencySymbol, Quote>> GetCryptoCurrencyQuotesAsync(CryptoCurrencyQuotesLookupDto quotesLookup, CancellationToken cancellationToken)
    {
        var quotes = new Dictionary<CurrencySymbol, Quote>();
        foreach (CurrencySymbol symbol in quotesLookup.TargeCurrencySymbols)
        {
            var quote = await GetCryptoCurrencyQuoteAsync(quotesLookup.SourceCryptoCurrencySymbol, symbol, cancellationToken);
            quotes.Add(symbol, quote);
        }

        return quotes;
    }

    private async Task<List<CryptoCurrencyDto>> GetListAsync(CancellationToken cancellationToken)
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
            throw new CryptoCurrencyException(ex.Message, ex);
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

            double? price =
                result
                .Data?[sourceCryptoCurrencySymbol.Symbol]
                .FirstOrDefault(currency => sourceCryptoCurrencySymbol.Symbol.Equals(currency.Symbol))?
                .Quote?[targeCurrencySymbol.Symbol]?.
                Price;

            return new Quote(price);
        }
        catch (ApiException ex)
        {
            throw ex.ToCryptoCurrencyException();
        }
        catch (TimeoutRejectedException ex)
        {
            throw new CryptoCurrencyException(ex.Message, ex);
        }
    }
}