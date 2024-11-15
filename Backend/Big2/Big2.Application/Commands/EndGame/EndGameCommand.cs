namespace Big2.Application.Commands.EndGame;

public class EndGameCommand : IRequest
{
    public required Guid GameId { get; init; }
}
