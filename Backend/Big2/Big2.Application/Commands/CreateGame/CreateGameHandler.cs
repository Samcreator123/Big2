namespace Big2.Application.Commands.CreateGame;

public class CreateGameHandler(IRepository<Game> repository) : IRequestHandler<CreateGameCommand, bool>
{
    public async Task<bool> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        Game game = new(request.GameName, request.IncludeJoker, request.MaxPlayers,
            request.PlayUntilLast, request.Handtypes);

        await repository.Save(game, cancellationToken);

        return true;
    }
}
