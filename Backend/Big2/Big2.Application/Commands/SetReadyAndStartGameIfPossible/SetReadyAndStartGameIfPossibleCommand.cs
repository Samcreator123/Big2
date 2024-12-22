namespace Big2.Application.Commands.SetReadyAndStartGameIfPossible;

public class SetReadyAndStartGameIfPossibleCommand(
    Guid gameId,
    Guid PlayerId) : IRequest<SetReadyAndStartGameIfPossibleState>
{
    public Guid GameId { get; init; } = gameId;
    public Guid PlayerId { get; init; } = PlayerId;
}
