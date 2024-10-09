namespace Big2.Application.SetReady
{
    public record SetReadyResponse(
        Guid PlayerID,
        bool IsReady,
        SetReadyState State,
        string additionalMessage)
    {
    }
}
