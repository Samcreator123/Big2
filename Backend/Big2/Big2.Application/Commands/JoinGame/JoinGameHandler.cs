namespace Big2.Application.Commands.JoinGame;

public class JoinGameHandler(IRepository<Game> repository) : IRequestHandler<JoinGameCommand>
{
    public async Task Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
            ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        game.JoinGame(request.GamerName);

        await repository.Save(game, cancellationToken);
    }
}
