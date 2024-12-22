namespace Big2.Application.Commands.PlayCardAndEndGameIfPossible;

public class PlayCardAndEndGameIfPossibleCommand(
    Guid gameId,
    Guid playerId,
    List<Card> cards,
    bool hasPassed) : IRequest<PlayCardAndEndGameIfPossibleState>
{
    public Guid GameId { get; init; } = gameId;

    public Guid PlayerId { get; init; } = playerId;

    public List<Card> Cards { get; init; } = cards;

    public bool HasPassed { get; init; } = hasPassed;
}
