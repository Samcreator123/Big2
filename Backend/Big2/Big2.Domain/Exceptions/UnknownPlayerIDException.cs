namespace Big2.Domain.Exceptions;

public class UnknownPlayerIDException(string? message) : GameException(message)
{
}
