namespace Big2.Domain.Exceptions;

public class InvalidPlayerCountException(string? message) : GameException(message)
{
}
