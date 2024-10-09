using Big2.Domain.Games;

namespace Big2.Application.CheckStartConditions
{
    public interface ICheckStartConditionsRepository
    {
        Game FindGameByPlayerID(Guid playerID);

        List<Guid> FindPreparedPlayerInGame(Guid GameID);
    }
}
