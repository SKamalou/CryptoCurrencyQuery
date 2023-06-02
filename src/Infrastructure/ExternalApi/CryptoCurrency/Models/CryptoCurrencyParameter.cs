using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
internal class CryptoCurrencyParameter
{
    [AliasAs("symbol")]
    public string SourceSymbol { get; set; }

    [AliasAs("convert")]
    public string TargetSymbol { get; set; }
}
