namespace Big2.Application.Commands.DeleteGame;

public class DeleteGameHandler(IRepository<Game> repository) : IRequestHandler<DeleteGameCommand>
{
    public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
                ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        await repository.Delete(game, cancellationToken);
    }
}
