namespace Big2.Application.CheckScoringConditions
{
    public record CheckScoringConditionResponse(
        Guid GameID,
        CheckScoringConditionState State,
        string AdditionalMessage)
    {
    }
}
