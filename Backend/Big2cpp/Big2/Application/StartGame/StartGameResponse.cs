
using Big2.Domain.Games;

namespace Big2.Application.StartGame
{
    public record StartGameResponse(
        Game game,
        List<Domain.Players.Player> Players,
        StartGameState State,
        string AdditionalMessage)
    {
    }
}
