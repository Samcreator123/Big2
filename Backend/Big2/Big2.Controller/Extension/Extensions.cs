
namespace Big2.Controller.Extension;

public static class Extensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            // 替IMediator注入，以處理IRequest
            config.RegisterServicesFromAssemblyContaining(typeof(Program));
            // 在請求的pipeline，新增紀錄log的behavior(IPipelineBehavior)
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddScoped<Big2Queries, Big2Queries>();
        services.AddScoped<IRepository<Game>, GameRepository>();
        services.AddScoped<IRepository<Player>, PlayerRepository>();
    }

    public static void AddOpenApiDoc(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseSwaggerInDevelopment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
