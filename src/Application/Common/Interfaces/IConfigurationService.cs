namespace CryptoCurrencyQuery.Application.Common.Interfaces;
public interface IConfigurationService
{
    T GetConfig<T>(string? key = null) where T : class, new();
}