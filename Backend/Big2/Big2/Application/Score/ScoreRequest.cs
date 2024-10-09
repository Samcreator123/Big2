namespace Big2.Application.Score
{
    public record ScoreRequest(
        Guid GameID,
        Guid FinishedPlayerID)
    {
    }
}
