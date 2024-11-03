namespace Big2.Application.Commands.DeleteGame;

public class DeleteGameCommand : IRequest<bool>
{
    public Guid GameId { get; init; }
}
