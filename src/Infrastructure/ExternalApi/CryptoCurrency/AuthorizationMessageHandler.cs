using System.Net.Http.Headers;
using CryptoCurrencyQuery.Infrastructure.Common;
using CryptoCurrencyQuery.Infrastructure.Configs;
using Microsoft.Extensions.Configuration;

namespace CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
public class AuthorizationMessageHandler : DelegatingHandler
{
    private const string ApiKeyHeaderKey = "X-CMC_PRO_API_KEY";
    private readonly IConfiguration _configuration;

    public AuthorizationMessageHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelToken)
    {
        HttpRequestHeaders headers = request.Headers;

        if (!headers.Contains(ApiKeyHeaderKey))
        {
            CryptoCurrencyApiConfig api = _configuration.BindTo<CryptoCurrencyApiConfig>();

            headers.Add(ApiKeyHeaderKey, api.ApiKey);
        }

        return await base.SendAsync(request, cancelToken);
    }
}