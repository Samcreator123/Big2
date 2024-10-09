namespace Big2.Application.SetNotReady
{
    public record SetNotReadyResponse(
        Guid PlayerID,
        bool IsReady,
        SetNotReadyState State,
        string additionalMessage)
    {
    }
}
