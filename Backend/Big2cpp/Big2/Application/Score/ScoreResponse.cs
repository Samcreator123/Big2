namespace Big2.Application.Score
{
    public record ScoreResponse(
        Guid GameID,
        Guid FinishedPlayerID,
        int Score,
        ScoreState State,
        string AdditionalMessage = "")
    {

    }
}
