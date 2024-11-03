namespace Big2.Domain.Exceptions;

public class InvalidGameStateException(string? message) : GameException(message)
{
}
