using Big2.Domain.Players;

namespace Big2.Application.SetNotReady
{
    public interface ISetNotReadyRepository
    {
        Player FindPlayerByID(Guid id);

        void SetPlayerNotReady(Player player);
    }
}
