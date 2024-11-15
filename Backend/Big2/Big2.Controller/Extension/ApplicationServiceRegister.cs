namespace Big2.Controller.Extension;

public static class ApplicationServiceRegister
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped<IRepository<Game>, GameRepository>();
        services.AddScoped<IRepository<Player>, PlayerRepository>();
    }
}
