namespace Big2.Domain.Exceptions;

public class InvalidPlayerStateException(string? message) : GameException(message)
{
}
