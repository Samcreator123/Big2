namespace Big2.Domain.Games.Exceptions
{
    public class GameInProgressException(string? message) : System.Exception(message)
    {
    }
}
