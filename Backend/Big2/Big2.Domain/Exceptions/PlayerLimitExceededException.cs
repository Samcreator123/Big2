namespace Big2.Domain.Exceptions;

public class PlayerLimitExceededException(string? message) : GameException(message)
{
}
