using Big2.Application.Commands.CreateGame;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Big2.Controller.Apis;

public static class Big2Api
{
    public static RouteGroupBuilder MapBig2ApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/big2").HasApiVersion(1.0);

        return api;
    }

    // 使用 Results 表示可以返回多種類型的結果
    // Ok -> 200 請求成功且不需要返回額外數據,
    // BadRequest -> 400 輸入驗證失敗或其他用戶端錯誤,
    // ProblemHttpRequest -> 500 伺服器端錯誤發生時返回
    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateGameAsync(
        CreateGameCommand command,
        [AsParameters] Big2Service services)
    {
        
        services.Logger.LogInformation($"執行 {nameof(command)}，參數為{command.ToString()}");

        await services.CommandMediator.Send(command);

        services.Logger.LogInformation($"執行完畢 {nameof(command)}，參數為{command.ToString()}");


        return TypedResults.Ok();
    }
}
