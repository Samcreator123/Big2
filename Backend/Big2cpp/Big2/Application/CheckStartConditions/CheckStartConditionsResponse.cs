namespace Big2.Application.CheckStartConditions
{
    public record CheckStartConditionsResponse(
        Guid GameID,
        CheckStartConditionsState State,
        string AdditionalMessage)
    {
    }
}
