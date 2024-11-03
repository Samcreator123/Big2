namespace Big2.Domain.Exceptions;

public class PlayedHandTooWeakException(string? message) : GameException(message)
{
}
