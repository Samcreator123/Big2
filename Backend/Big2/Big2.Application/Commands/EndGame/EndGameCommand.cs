namespace Big2.Application.Commands.EndGame;

public class EndGameCommand : IRequest<bool>
{
    public required Guid GameId { get; init; }
}
