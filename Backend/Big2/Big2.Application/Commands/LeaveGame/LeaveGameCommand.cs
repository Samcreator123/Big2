namespace Big2.Application.Commands.LeaveGame;

public class LeaveGameCommand : IRequest<bool>
{
    public Guid GameId { get; init; }
    
    public Guid PlayerId { get; init; }
}
