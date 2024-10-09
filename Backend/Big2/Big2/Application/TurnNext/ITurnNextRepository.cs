using Big2.Domain.Games;

namespace Big2.Application.TurnNext
{
    public interface ITurnNextRepository
    {
        Game FindGameByID(Guid gameID);
    }
}
