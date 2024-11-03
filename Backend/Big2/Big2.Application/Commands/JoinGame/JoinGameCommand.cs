namespace Big2.Application.Commands.JoinGame;

public class JoinGameCommand : IRequest<bool>
{
    public required Guid GameId { get; init; }

    public required string GamerName { get; init; }
}
