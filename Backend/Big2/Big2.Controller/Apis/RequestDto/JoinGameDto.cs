namespace Big2.Controller.Apis.RequestDto;

public class JoinGameDto
{
    public required string GameName { get; init; }

    public required Guid GameId { get; init; }
}
