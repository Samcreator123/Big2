using Big2.Domain.Players;

namespace Big2.Application.GetPlayer
{
    public interface IGetPlayerRepository
    {
        Player? GetPlayer(Guid PlayerID);

    }
}
