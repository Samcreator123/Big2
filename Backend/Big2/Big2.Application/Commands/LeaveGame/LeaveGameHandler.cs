namespace Big2.Application.Commands.LeaveGame;

public class LeaveGameHandler(IRepository<Game> repository) : IRequestHandler<LeaveGameCommand, bool>
{
    public async Task<bool> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
                ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        game.LeaveGame(request.PlayerId);

        await repository.Save(game, cancellationToken);

        return true;
    }
}
