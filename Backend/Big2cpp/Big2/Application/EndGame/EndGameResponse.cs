namespace Big2.Application.EndGame
{
    public record EndGameResponse(
        Guid GameID,
        EndGameState State,
        string AdditionalMessage = "")
    {
    }
}
