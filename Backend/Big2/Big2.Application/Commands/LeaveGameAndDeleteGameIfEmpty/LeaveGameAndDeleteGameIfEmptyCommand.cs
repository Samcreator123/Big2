namespace Big2.Application.Commands.LeaveGameAndDeleteGameIfEmpty;

public class LeaveGameAndDeleteGameIfEmptyCommand(Guid gameId, Guid playerId) 
    : IRequest<LeaveGameAndDeleteGameIfEmptyState>
{
    public Guid GameId { get; init; } = gameId;

    public Guid PlayerId { get; init; } = playerId;
}
