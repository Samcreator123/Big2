

namespace Big2.Controller.Apis;

public class Big2Api : Hub
{
    static readonly Rooms _rooms = new();
    //public static RouteGroupBuilder MapBig2ApiV1(this IEndpointRouteBuilder app)
    //{
    //    var api = app.MapGroup("api/big2").HasApiVersion(1.0);
    //    api.MapPost("/CreateGame", CreateGameAsync);

    //    return api;
    //}

    public async Task<IResult> CreateGameAsync(
        CreateGameDto request,
        [AsParameters] Big2Service services,
        CancellationToken cancellationToken)
    {
        Guid newGameId = Guid.NewGuid();

        CreateGameCommand createGameCommand = new(
            newGameId, 
            request.GameName, 
            request.IncludeJoker, 
            request.MaxPlayers, 
            request.PlayUntilLast, 
            request.Handtypes);

        await services.CommandMediator.Send(createGameCommand, cancellationToken);

        // 要沉下去application用transation方式做
        JoinGameCommand joinGameCommand = new(newGameId, request.CreatorName);

        await services.CommandMediator.Send(joinGameCommand, cancellationToken);

        var game = await services.Queries.GetGame(newGameId, cancellationToken);

        _rooms.AddRoom(game.Id, game.Players.First().Id, Context.ConnectionId);

        return TypedResults.Ok(game);
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
