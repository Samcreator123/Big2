namespace Big2.Domain.Players.Exceptions
{
    public class NullCurrentCombinationException(string? message) : PlayCardException(message)
    {
    }
}
