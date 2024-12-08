namespace Big2.Application.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Starting Request : {RequestName} at {DateTime}", requestName, DateTime.UtcNow);

        try
        {
            var response = await next();
 
            _logger.LogInformation("Compeleted Request : {RequestName} at {DateTime}", requestName, DateTime.UtcNow);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Request {RequestName} failed at {DateTime}, Parameter is {Parameters}", 
                             requestName, 
                             DateTime.UtcNow, 
                             request.ToString());
            throw;
        }
    }
}
