using Big2.Domain.Players;

namespace Big2.Application.Score
{
    public interface IScoreRepository
    {
        Player GetFinishedPlayer(Guid finishedPlayerID);

        List<Player> GetUnFinishPlayers(Guid GameID);

        void Save(Player player);
    }
}
