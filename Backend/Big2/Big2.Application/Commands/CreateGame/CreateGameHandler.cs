namespace Big2.Application.Commands.CreateGame;

public class CreateGameHandler(IRepository<Game> repository) : IRequestHandler<CreateGameCommand>
{
    public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        Game game = new(request.Id, request.GameName, request.IncludeJoker, request.MaxPlayers,
            request.PlayUntilLast, request.Handtypes);

        game.JoinGameAndGetPlayerId(request.GameName);

        await repository.Save(game, cancellationToken);
    }
}
