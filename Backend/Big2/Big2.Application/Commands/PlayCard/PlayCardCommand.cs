namespace Big2.Application.Commands.PlayCard;

public class PlayCardCommand : IRequest<bool>
{
    public Guid GameId { get; init; }

    public Guid PlayerId { get; init; }

    public List<Card> Cards { get; init; } = [];

    public bool HasPass { get; init; }
}
