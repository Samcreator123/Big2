namespace Big2.Domain.Games.Exceptions
{
    public class InvalidGameStateException(string? message) : System.Exception(message)
    {
    }
}
