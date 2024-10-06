using Big2.Domain.Services.Decks;
using Big2.Domain.Services.Rules;
using Big2.Domain.Services.Rules.CardCombinationRules;


namespace Big2.Domain.Services.Scoring
{
    public class FinishedPlayerScoring
    {
        public static int CalculateMultiplier(Combination lastCombination)
        {
            bool hasJoker = lastCombination.Cards.Any(c => c == Card.GetJokerCard());
            bool hasMaxCard = lastCombination.Cards.Any(c => c == Card.GetMaxCard());
            int multiplier = 1;
            if (hasJoker)
            {
                multiplier *= 2;
            }

            if (hasMaxCard)
            {
                multiplier *= 2;
            }

            bool isMonsterType = CombinationRuleHandler.IsMonsterType(lastCombination.Type);
            if (!hasJoker && !hasMaxCard && isMonsterType)
            {
                multiplier *= 2;
            }

            return multiplier;
        }
    }
}
