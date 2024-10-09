using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules
{
    public class StraightFlush : ICombinationRule
    {
        private readonly Straight _straight = new();

        private readonly int _validCount = 5;

        public int GetValidCount()
        {
            return _validCount;
        }

        public CardCombinationTypes GetCombinationType()
        {
            return CardCombinationTypes.StraightFlush;
        }

        public bool IsValid(List<Card> cards)
        {
            if (!IsSameSuit(cards))
            {
                return false;
            }

            return _straight.IsValid(cards);
        }

        private bool IsSameSuit(List<Card> cards)
        {
            return cards
                .GroupBy(o => o.Suit)
                .Max(g => g.Count()) == cards.Count;
        }

        public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
        {
            return _straight.IsFirstGreater(currCards, comparedCards);
        }
    }
}
