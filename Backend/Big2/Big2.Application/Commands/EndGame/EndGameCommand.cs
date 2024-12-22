namespace Big2.Application.Commands.EndGame;

public class EndGameCommand(Guid gameId) : IRequest
{
    public Guid GameId { get; init; } = gameId;
}
