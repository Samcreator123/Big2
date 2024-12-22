namespace Big2.Controller.Apis.RequestDto;

public class PlayCardDto
{
    public required Guid GameId { get; init; }

    public required Guid PlayerId { get; init; }

    public required List<Card> Cards { get; init; } = [];

    public required bool HasPassed { get; init; }
}
