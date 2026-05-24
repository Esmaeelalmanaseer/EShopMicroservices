using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[START] Handling {RequestName} -> {ResponseName}. RequestData: {@Request}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request);                                                                                                                                                                              

        var timer = Stopwatch.StartNew();

        var response = await next();

        timer.Stop();

        if (timer.Elapsed.TotalSeconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] {RequestName} took {ElapsedSeconds:F2}s",
                typeof(TRequest).Name,
                timer.Elapsed.TotalSeconds);
        }

        logger.LogInformation(
            "[END] Handled {RequestName} -> {ResponseName}. ResponseData: {@Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            response);

        return response;
    }
}