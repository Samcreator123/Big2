using Big2.Domain.Players;

namespace Big2.Application.EndPlayer
{
    public interface IEndPlayerRepository
    {
        Player GetPlayer(Guid PlayerID);

        void Save(Player player);
    }
}
