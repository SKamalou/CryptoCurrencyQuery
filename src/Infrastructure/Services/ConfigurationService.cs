using CryptoCurrencyQuery.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CryptoCurrencyQuery.Infrastructure.Services;
internal class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T GetConfig<T>(string? key = null) where T : class, new()
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        T bindingObject = new T();

        _configuration.GetSection(key).Bind(bindingObject);

        return bindingObject;
    }
}