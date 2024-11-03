namespace Big2.Domain.Service.Scoring;

public class ScoringHandler
{
    public static int CalculateScore(ValidHand lastHand, List<List<Card>> unfinishedCardLeft)
    {
        if (unfinishedCardLeft.Count == 0)
        {
            return 0;
        }

        return unfinishedCardLeft.Sum(UnfinishedPlayerScoring.Calculate)
            * FinishedPlayerScoring.CalculateMultiplier(lastHand);
    }
}
