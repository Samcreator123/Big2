using Big2.Domain.Games;

namespace Big2.Application.GetGame
{
    public record GetGameResponse(
        Game? Game, 
        GetGameState State,
        string AdditionalMessage = ""))
    {
    }
}
