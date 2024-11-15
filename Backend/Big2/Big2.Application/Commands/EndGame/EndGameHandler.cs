namespace Big2.Application.Commands.EndGame;

public class EndGameHandler(IRepository<Game> repository) : IRequestHandler<EndGameCommand>
{
    public async Task Handle(EndGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
            ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        // 目前結束後沒有要做什麼事，所以直接進入等待狀態.
        game.SetWaiting();

        await repository.Save(game, cancellationToken);
    }
}
