namespace Big2.Domain.Exceptions;

public class NotFoundPlayerException(string? message) : GameException(message)
{
}
