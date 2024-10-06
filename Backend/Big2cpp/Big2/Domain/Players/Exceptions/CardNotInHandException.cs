namespace Big2.Domain.Players.Exceptions
{
    public class CardNotInHandException(string? message) : PlayCardException(message)
    {
    }
}
