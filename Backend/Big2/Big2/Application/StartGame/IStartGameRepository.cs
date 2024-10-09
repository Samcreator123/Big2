using Big2.Domain.Games;
using Big2.Domain.Players;

namespace Big2.Application.StartGame
{
    public interface IStartGameRepository
    {
        Game FindGameByID(Guid gameID);

        List<Domain.Players.Player> FindAllPlayersInGame(Guid gameID);

        void Save(Game game);

        void Save(List<Domain.Players.Player> players);
        
    }
}
