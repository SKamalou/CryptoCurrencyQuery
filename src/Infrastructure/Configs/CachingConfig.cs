namespace CryptoCurrencyQuery.Infrastructure.Configs;
public record CachingConfig
{
    public double CryptoCurrenciesExpireTimeInMinute { get; set; }
}