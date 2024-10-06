using Big2.Domain.Games;

namespace Big2.Application.CheckScoringConditions
{
    public interface ICheckScoringConditionRepository
    {
        Game FindGameByID(Guid gameID);

        void Save(Game game);
    }
}
