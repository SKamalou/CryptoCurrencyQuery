using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
internal class CryptoCurrencyResponseStatus
{
    [JsonPropertyName("Error_Code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("Error_Message")]
    public string ErrorMessage { get; set; }
}
