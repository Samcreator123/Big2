namespace Big2.Application.Commands.PlayCardAndEndGameIfPossible;

public class PlayCardAndEndGameIfPossibleHandler(IRepository<Game> repository) 
    : IRequestHandler<PlayCardAndEndGameIfPossibleCommand, PlayCardAndEndGameIfPossibleState>
{
    public async Task<PlayCardAndEndGameIfPossibleState> Handle(
        PlayCardAndEndGameIfPossibleCommand request, 
        CancellationToken cancellationToken)
    {
        Game game = await GetGame(repository, request.GameId, cancellationToken);

        game.ProcessCardPlayAndAddScoreIfPossible(request.PlayerId, request.HasPassed, request.Cards);

        bool canGameEnded = game.CanEnd();

        if (canGameEnded)
        {
            game.SetWaiting();
        }

        await repository.Save(game, cancellationToken);

        return canGameEnded ?
            PlayCardAndEndGameIfPossibleState.CardPlayedAndGameEnded :
            PlayCardAndEndGameIfPossibleState.OnlyCardPlayed;
    }

    private static async Task<Game> GetGame(IRepository<Game> repository, Guid gameId, CancellationToken cancellationToken)
    {
        return await repository.FindById(gameId, cancellationToken) ??
            throw new NotFoundGameException($"找不到遊戲 {gameId}");
    }

}
