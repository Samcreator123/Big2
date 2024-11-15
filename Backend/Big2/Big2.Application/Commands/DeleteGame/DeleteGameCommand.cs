namespace Big2.Application.Commands.DeleteGame;

public class DeleteGameCommand : IRequest
{
    public Guid GameId { get; init; }
}
