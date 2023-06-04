using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public class CryptoCurrencyParameter
{
    [AliasAs("symbol")]
    public string SourceSymbol { get; set; }

    [AliasAs("convert")]
    public string TargetSymbol { get; set; }
}
