namespace Big2.Application.Commands.SetNotReady;

public class SetNotReadyHandler(IRepository<Player> repository) : IRequestHandler<SetNotReadyCommand, bool>
{
    public async Task<bool> Handle(SetNotReadyCommand request, CancellationToken cancellationToken)
    {
        var player = await repository.FindById(request.PlayerId, cancellationToken) ??
            throw new NotFoundPlayerException($"找不到玩家 {request.PlayerId}");

        player.SetNotReady();

        await repository.Save(player, cancellationToken);

        return true;
    }
}
