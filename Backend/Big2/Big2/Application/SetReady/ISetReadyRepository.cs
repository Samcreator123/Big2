using Big2.Domain.Players;

namespace Big2.Application.SetPlayerReady
{
    public interface ISetReadyRepository
    {
        Player FindPlayerByID(Guid id);

        void SetPlayerReady(Player player);
    }
}
