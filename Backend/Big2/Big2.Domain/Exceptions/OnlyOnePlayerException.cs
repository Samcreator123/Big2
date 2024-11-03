namespace Big2.Domain.Exceptions;

public class OnlyOnePlayerException(string? message) : GameException(message)
{
}
