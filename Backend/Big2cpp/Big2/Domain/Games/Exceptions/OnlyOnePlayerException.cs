namespace Big2.Domain.Games.Exceptions
{
    public class OnlyOnePlayerException(string? message) : Exception(message)
    {
    }
}
