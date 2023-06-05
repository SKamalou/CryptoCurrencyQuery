namespace CryptoCurrencyQuery.Infrastructure.Configs;
internal record WaitAndRetryConfig
{
    public int Retry { get; set; }
    public int Wait { get; set; }
    public int Timeout { get; set; }
}