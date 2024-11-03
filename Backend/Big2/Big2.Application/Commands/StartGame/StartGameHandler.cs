using Big2.Domain.Service.Deck;

namespace Big2.Application.Commands.StartGame;

public class StartGameHandler(IRepository<Game> repository) : IRequestHandler<StartGameCommand, bool>
{
    public async Task<bool> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        var game = await repository.FindById(request.GameId, cancellationToken)
            ?? throw new NotFoundGameException($"找不到遊戲 {request.GameId}");

        var idAndCards = Deck.ShuffleAndDeal(game.CustomSetting.IncludeJoker, new(game.Players.Select(o => o.Id)));

        foreach (var idAndCard in idAndCards)
        {
            var player = game.GetPlayerById(idAndCard.Key);
            
            player.SetPlaying(idAndCard.Value);
        }

        game.SetPlaying();

        await repository.Save(game, cancellationToken);

        return true;
    }
}
