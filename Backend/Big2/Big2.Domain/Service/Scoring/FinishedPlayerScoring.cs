namespace Big2.Domain.Service.Scoring;

public class FinishedPlayerScoring
{
    public static int CalculateMultiplier(ValidHand lastHand)
    {
        bool hasJoker = lastHand.Cards.Any(c => c == Card.GetJokerCard());
        bool hasMaxCard = lastHand.Cards.Any(c => c == Card.GetMaxCard());
        int multiplier = 1;
        if (hasJoker)
        {
            multiplier *= 2;
        }

        if (hasMaxCard)
        {
            multiplier *= 2;
        }

        bool isMonsterType = HandRuleHandler.IsMonsterType(lastHand.Type);
        if (!hasJoker && !hasMaxCard && isMonsterType)
        {
            multiplier *= 2;
        }

        return multiplier;
    }
}
