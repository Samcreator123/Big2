namespace Big2.Domain.Players.Exceptions
{
    public class PlayedCombinationTooWeakException(string? message) : PlayCardException(message)
    {
    }
}
