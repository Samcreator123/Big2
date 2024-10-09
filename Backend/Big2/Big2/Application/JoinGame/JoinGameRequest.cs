namespace Big2.Application.JoinGame
{
    public record JoinGameRequest(
        Guid GameID,
        string GamerName)
    {
    }
}
