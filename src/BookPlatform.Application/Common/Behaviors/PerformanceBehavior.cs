using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookPlatform.Application.Common.Behaviors;

public sealed class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to process {RequestType}", typeof(TRequest).Name);
        var sw = Stopwatch.StartNew();

        var response = await next();

        sw.Stop();

        _logger.LogInformation("Processed {RequestType} in {ElapsedMilliseconds}ms", typeof(TRequest).Name,
            sw.ElapsedMilliseconds);

        return response;
    }
}