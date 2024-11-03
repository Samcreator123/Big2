namespace Big2.Domain.Exceptions;

public class CardNotInHandException(string? message) : GameException(message)
{
}
