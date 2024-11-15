namespace Big2.Application.Commands.CreateGame;

public class CreateGameHandler(IRepository<Game> repository) : IRequestHandler<CreateGameCommand>
{
    public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        Game game = new(request.GameName, request.IncludeJoker, request.MaxPlayers,
            request.PlayUntilLast, request.Handtypes);

        await repository.Save(game, cancellationToken);
    }
}
