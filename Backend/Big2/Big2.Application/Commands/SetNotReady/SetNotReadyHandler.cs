namespace Big2.Application.Commands.SetNotReady;

public class SetNotReadyHandler(IRepository<Player> repository) : IRequestHandler<SetNotReadyCommand>
{
    public async Task Handle(SetNotReadyCommand request, CancellationToken cancellationToken)
    {
        var player = await repository.FindById(request.PlayerId, cancellationToken) ??
            throw new NotFoundPlayerException($"找不到玩家 {request.PlayerId}");

        player.SetNotReady();

        await repository.Save(player, cancellationToken);
    }
}
