namespace Big2.Application.Commands.JoinGame;

public class JoinGameHandler(IRepository<Game> repository) : IRequestHandler<JoinGameCommand, Guid>
{
    public async Task<Guid> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
            ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        Guid playerId = game.JoinGameAndGetPlayerId(request.GamerName);

        await repository.Save(game, cancellationToken);

        return playerId;
    }
}
