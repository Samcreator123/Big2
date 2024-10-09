using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules
{
    public class Straight : ICombinationRule
    {
        private readonly static StraightRanker _straightRanker = new();

        private readonly static int _validCount = 5;

        public int GetValidCount()
        {
            return _validCount;
        }

        public CardCombinationTypes GetCombinationType()
        {
            return CardCombinationTypes.Straight;
        }

        public bool IsValid(List<Card> cards)
        {
            if (IsValidRankDistribution(cards))
            {
                return _straightRanker.GetStraight(cards).Count != 0;
            }
            else
            {
                return false;
            }
        }

        private bool IsValidRankDistribution(List<Card> cards)
        {
            var suitCounts = cards.GroupBy(c => c.Rank)
                .Select(g => new { key = g.Key, count = g.Count() })
                .OrderByDescending(g => g.count)
                .ToList();

            if (suitCounts[0].count > 2 ||
               suitCounts[0].count == 2 && suitCounts[0].key != Rank.Joker)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
        {
            var currentStraight = _straightRanker.GetOrderAndStraight(currCards);

            var comparedStraight = _straightRanker.GetOrderAndStraight(comparedCards);

            if (currentStraight.order == comparedStraight.order)
            {
                var currentLastSuit = currentStraight.sequence.Last().Suit;
                var comparedLastSuit = comparedStraight.sequence.Last().Suit;

                return currentLastSuit > comparedLastSuit;
            }
            else
            {
                return currentStraight.order > comparedStraight.order;
            }
        }
    }
}
