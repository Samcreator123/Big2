using Big2.Domain.Service.Deck;

namespace Big2.Application.Commands.SetReadyAndStartGameIfPossible;

public class SetReadyAndStartGameIfPossibleHandler(IRepository<Game> repository)
     : IRequestHandler<SetReadyAndStartGameIfPossibleCommand, SetReadyAndStartGameIfPossibleState>
{
    public async Task<SetReadyAndStartGameIfPossibleState> Handle(
        SetReadyAndStartGameIfPossibleCommand request, 
        CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken) ??
             throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        var player = game.Players.FirstOrDefault(p => p.Id == request.PlayerId) ??
            throw new NotFoundPlayerException($"找不到玩家 {request.PlayerId}");

        player.SetReady();

        bool canStart = game.CanStart();

        if (canStart)
        {
            game.SetPlaying();
        }

        await repository.Save(game, cancellationToken);

        return canStart ? 
            SetReadyAndStartGameIfPossibleState.SetReadyAndStartGame : 
            SetReadyAndStartGameIfPossibleState.SetReadyOnly;
    }
}
