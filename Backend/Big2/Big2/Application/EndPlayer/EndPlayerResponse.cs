namespace Big2.Application.EndPlayer
{
    public record EndPlayerResponse(
        Guid PlayerID,
        EndPlayerState State,
        string AdditionalMessage = "")
    {
    }
}
