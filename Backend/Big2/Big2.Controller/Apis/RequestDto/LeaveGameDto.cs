namespace Big2.Controller.Apis.RequestDto;

public class LeaveGameDto
{
    public required Guid GameId { get; init; }

    public required Guid PlayerId { get; init; }
}
