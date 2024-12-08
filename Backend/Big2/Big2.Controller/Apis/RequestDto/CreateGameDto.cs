using Big2.Domain.Enums;

namespace Big2.Controller.Apis.RequestDto;

public class CreateGameDto
{
    public required string GameName { get; init; }

    public required string CreatorName { get; init; }

    public bool IncludeJoker { get; init; }

    public int MaxPlayers { get; init; }

    public bool PlayUntilLast { get; init; }

    public required List<ValidHandType> Handtypes { get; init; }
}
