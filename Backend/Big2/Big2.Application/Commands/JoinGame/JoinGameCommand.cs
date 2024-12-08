namespace Big2.Application.Commands.JoinGame;

public class JoinGameCommand(Guid gameId, string gamerName) : IRequest
{
    public Guid GameId { get; init; } = gameId;

    public string GamerName { get; init; } = gamerName;
}
