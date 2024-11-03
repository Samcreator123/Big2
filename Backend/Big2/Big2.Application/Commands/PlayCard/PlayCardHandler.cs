namespace Big2.Application.Commands.PlayCard;

public class PlayCardHandler(IRepository<Game> repository) : IRequestHandler<PlayCardCommand, bool>
{
    public async Task<bool> Handle(PlayCardCommand request, CancellationToken cancellationToken)
    {
        Game game = await GetGame(repository, request.GameId, cancellationToken);

        game.ProcessCardPlay(request.PlayerId, request.HasPass, request.Cards);

        await repository.Save(game, cancellationToken);

        return true;
    }

    private static async Task<Game> GetGame(IRepository<Game> repository, Guid gameId, CancellationToken cancellationToken)
    {
        return await repository.FindById(gameId, cancellationToken) ??
            throw new NotFoundGameException($"找不到遊戲 {gameId}");
    }

}
