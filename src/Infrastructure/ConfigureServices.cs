using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Infrastructure.Common;
using CryptoCurrencyQuery.Infrastructure.Configs;
using CryptoCurrencyQuery.Infrastructure.ExternalApi.CryptoCurrency;
using CryptoCurrencyQuery.Infrastructure.Persistence;
using CryptoCurrencyQuery.Infrastructure.Persistence.Interceptors;
using CryptoCurrencyQuery.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Refit;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CryptoCurrencyQueryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        var waitAndRetryConfig = configuration.BindTo<WaitAndRetryConfig>();

        AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>() // Thrown by Polly's TimeoutPolicy if the inner call gets timeout.
            .WaitAndRetryAsync(waitAndRetryConfig.Retry, _ => TimeSpan.FromMilliseconds(waitAndRetryConfig.Wait));

        AsyncTimeoutPolicy<HttpResponseMessage> timeoutPolicy = Policy
            .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(waitAndRetryConfig.Timeout));

        var apiConfig = configuration.BindTo<CryptoCurrencyApiConfig>();
        services.AddTransient<AuthorizationMessageHandler>();
        services.AddRefitClient<ICryptoCurrencyClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiConfig.BaseAddress))
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(timeoutPolicy)
            .AddHttpMessageHandler<AuthorizationMessageHandler>();

        services.AddTransient<ICryptoCurrencyService, CryptoCurrencyService>();

        services.AddDistributedMemoryCache();

        return services;
    }
}