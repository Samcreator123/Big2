namespace Big2.Application.Commands.JoinGame;

public class JoinGameCommand : IRequest
{
    public required Guid GameId { get; init; }

    public required string GamerName { get; init; }
}
