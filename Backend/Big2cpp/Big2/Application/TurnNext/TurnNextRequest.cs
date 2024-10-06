namespace Big2.Application.TurnNext
{
    public record TurnNextRequest(
        Guid GameID,
        Guid ThisPlayerID,
        string AdditionalMessage)
    {
    }
}
