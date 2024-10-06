using Big2.Domain.Games;

namespace Big2.Application.EndGame
{
    public interface IEndGameRepository
    {
        Game GetGame(Guid GameID);

        void Save(Game game);
    }
}
