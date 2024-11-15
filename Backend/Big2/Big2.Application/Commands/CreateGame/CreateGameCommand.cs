namespace Big2.Application.Commands.CreateGame;

public class CreateGameCommand : IRequest
{
    public required string GameName { get; init; }

    public required string CreatorName { get; init; }

    public bool IncludeJoker { get; init; }

    public int MaxPlayers { get; init; }

    public bool PlayUntilLast { get; init; }

    public required List<ValidHandType> Handtypes { get; init; }
}
