using Big2.Domain.Players;

namespace Big2.Domain.Services.Scoring
{
    public class ScoringHandler
    {
        public static int CalculateScore(Player finishedPlayer, List<Player> unFinishedPlayer)
        {
            if (unFinishedPlayer.Count == 0)
            {
                return 0;
            }

            return unFinishedPlayer.Sum(UnfinishedPlayerScoring.Calculate)
                * FinishedPlayerScoring.CalculateMultiplier(finishedPlayer.LastCombination);
        }
    }
}
