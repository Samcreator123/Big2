using Big2.Domain.Games;

namespace Big2.Application.TurnNext
{
    public record TurnNextResponse(
        Guid GameID,
        Guid NextPlayer,
        TurnNextState State,
        string AdditionalMessage)
    {
    }
}
