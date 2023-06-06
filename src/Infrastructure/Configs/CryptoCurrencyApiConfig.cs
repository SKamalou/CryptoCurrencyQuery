namespace CryptoCurrencyQuery.Infrastructure.Configs;
internal record CryptoCurrencyApiConfig
{
    public string BaseAddress { get; set; }
    public string ApiKeyHeaderKey { get; set; }
    public string ApiKey { get; set; }
}