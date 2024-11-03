namespace Big2.Domain.Exceptions;

public class GameInProgressException(string? message) : GameException(message)
{
}
