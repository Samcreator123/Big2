using Big2.Domain.Games;

namespace Big2.Application.GetGame
{
    public interface IGetGameRepository
    {
        Game? GetGame(Guid GameID);
    }
}
