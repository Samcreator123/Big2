namespace Big2.Application.CheckScoringConditions
{
    public record CheckScoringConditionsRequest(
        Guid GameID,
        Guid FinishedPlayerID)
    {
    }
}
