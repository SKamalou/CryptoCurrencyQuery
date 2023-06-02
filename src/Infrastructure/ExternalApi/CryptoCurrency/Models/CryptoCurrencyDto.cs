using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
internal class CryptoCurrencyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public Dictionary<string, QuoteInfo> Quote { get; set; }
}
