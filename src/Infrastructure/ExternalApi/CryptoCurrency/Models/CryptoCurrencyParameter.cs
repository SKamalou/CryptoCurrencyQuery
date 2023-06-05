using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public record CryptoCurrencyParameter
{
    [AliasAs("symbol")]
    public string SourceSymbol { get; set; }

    [AliasAs("convert")]
    public string TargetSymbol { get; set; }
}
