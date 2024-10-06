using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules
{
    public class Single : ICombinationRule
    {
        private readonly int _validCount = 1;

        public int GetValidCount()
        {
            return _validCount;
        }

        public bool IsValid(List<Card> cards)
        {
            return cards.Count == 1;
        }

        public CardCombinationTypes GetCombinationType()
        {
            return CardCombinationTypes.Single;
        }

        public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
        {
            Card maxCard = Card.GetMaxCard();

            Card firstCurr = currCards.TurnJokerAs(maxCard)[0];
            Card firstCompared = comparedCards.TurnJokerAs(maxCard)[0];

            return firstCurr.Compare(firstCompared) > 0;
        }
    }
}
