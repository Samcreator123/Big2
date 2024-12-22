namespace Big2.Application.Commands.CreateGame;

public class CreateGameCommand(Guid id,
    string gameName,
    bool includeJoker,
    int maxPlayers,
    bool playUntilLast,
    string creatorName,
    List<ValidHandType> handtypes) : IRequest
{
    public Guid Id { get; init; } = id;

    public string GameName { get; init; } = gameName;

    public bool IncludeJoker { get; init; } = includeJoker;

    public int MaxPlayers { get; init; } = maxPlayers;

    public bool PlayUntilLast { get; init; } = playUntilLast;

    public string CreatorNmae { get; init; } = creatorName;

    public List<ValidHandType> Handtypes { get; init; } = handtypes;
}
