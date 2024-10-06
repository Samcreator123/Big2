namespace Big2.Domain.Players.Exceptions
{
    public class InvalidFirstCardPlayException(string? message) : PlayCardException(message)
    {
    }
}
