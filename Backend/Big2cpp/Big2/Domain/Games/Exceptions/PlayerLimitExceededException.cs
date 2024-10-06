namespace Big2.Domain.Games.Exceptions
{
    public class PlayerLimitExceededException(string? message) : Exception(message)
    {
    }
}
