namespace Big2.Application.Commands.SetReady;

public class SetReadyHandler(IRepository<Player> repository) : IRequestHandler<SetReadyCommand, bool>
{
    public async Task<bool> Handle(SetReadyCommand request, CancellationToken cancellationToken)
    {
        var player = await repository.FindById(request.PlayerId, cancellationToken) ??
             throw new NotFoundPlayerException($"找不到玩家 {request.PlayerId}");

        player.SetReady();

        await repository.Save(player, cancellationToken);

        return true;
    }
}
