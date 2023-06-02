using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
internal class CryptoCurrencyQuotes
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public Dictionary<string, List<CryptoCurrencyDto>> Data { get; set; }
}
