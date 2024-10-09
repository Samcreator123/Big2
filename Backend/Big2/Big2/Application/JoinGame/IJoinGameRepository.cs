using Big2.Domain.Games;
using Big2.Domain.Players;

namespace Big2.Application.JoinGame
{
    public interface IJoinGameRepository
    {
        Game FindGameByID(Guid gameID);

        void AddAGamer(Game game);
    }
}
