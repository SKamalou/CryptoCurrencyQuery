using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CryptoCurrencyQuery.Application.Common.Behaviours;

internal class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("CryptoCurrencyQuery Request: {Name} {@Request}",requestName, request);

        return Task.CompletedTask;
    }
}