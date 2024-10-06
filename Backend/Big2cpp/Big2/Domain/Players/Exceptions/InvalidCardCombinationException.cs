namespace Big2.Domain.Players.Exceptions
{
    public class InvalidCardCombinationException(string? message) : PlayCardException(message)
    {
    }
}
