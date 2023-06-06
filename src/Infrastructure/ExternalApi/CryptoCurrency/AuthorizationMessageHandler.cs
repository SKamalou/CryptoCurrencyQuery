using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Infrastructure.Configs;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
internal class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly IConfigurationService _configurationService;

    public AuthorizationMessageHandler(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        var headers = request.Headers;
        var apiConfig = _configurationService.GetConfig<CryptoCurrencyApiConfig>();

        if (!headers.Contains(apiConfig.ApiKeyHeaderKey))
            headers.Add(apiConfig.ApiKeyHeaderKey, apiConfig.ApiKey);

        return await base.SendAsync(request, cancelToken);
    }
}