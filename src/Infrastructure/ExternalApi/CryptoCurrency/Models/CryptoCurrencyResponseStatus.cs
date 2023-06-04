using System.Text.Json.Serialization;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency.Models;
public class CryptoCurrencyResponseStatus
{
    [JsonPropertyName("Error_Code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("Error_Message")]
    public string ErrorMessage { get; set; }
}
