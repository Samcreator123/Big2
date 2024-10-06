using Big2.Domain.Games;

namespace Big2.Application.CreateGame
{
    public interface IBuildGameRepository
    {
        void SaveAsync(Game game);
    }
}
