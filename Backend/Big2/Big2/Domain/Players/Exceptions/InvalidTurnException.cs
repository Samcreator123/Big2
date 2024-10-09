namespace Big2.Domain.Players.Exceptions
{
    public class InvalidTurnException(string? message) : PlayCardException(message)
    {
    }
}
