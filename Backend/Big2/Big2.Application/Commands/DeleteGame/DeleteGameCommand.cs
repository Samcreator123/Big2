namespace Big2.Application.Commands.DeleteGame;

public class DeleteGameCommand(Guid gameId) : IRequest
{
    public Guid GameId { get; init; } = gameId;
}
