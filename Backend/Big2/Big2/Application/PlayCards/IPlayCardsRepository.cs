using Big2.Domain.Players;

namespace Big2.Application.PlayCards
{
    public interface IPlayCardsRepository
    {
        Player GetCurrentPlayer(Guid GameID);

        void Save(Player player);
    }
}
