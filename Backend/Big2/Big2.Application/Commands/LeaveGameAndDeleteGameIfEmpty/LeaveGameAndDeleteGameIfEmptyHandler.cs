namespace Big2.Application.Commands.LeaveGameAndDeleteGameIfEmpty;

public class LeaveGameAndDeleteGameIfEmptyHandler(IRepository<Game> repository) : 
    IRequestHandler<LeaveGameAndDeleteGameIfEmptyCommand, LeaveGameAndDeleteGameIfEmptyState>
{
    public async Task<LeaveGameAndDeleteGameIfEmptyState> Handle(
        LeaveGameAndDeleteGameIfEmptyCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
                ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        game.LeaveGame(request.PlayerId);

        if (game.Players.Count == 0)
        {
            await repository.Delete(game, cancellationToken);

            return LeaveGameAndDeleteGameIfEmptyState.PlayerLeftAndGameDeleted;
        }
        else
        {
            await repository.Save(game, cancellationToken);

            return LeaveGameAndDeleteGameIfEmptyState.OnlyPlayerLeft;
        }

    }
}
