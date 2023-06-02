using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
internal class CryptoCurrencyList
{
    public CryptoCurrencyResponseStatus Status { get; set; }
    public List<CryptoCurrencyDto> Data { get; set; }
}
