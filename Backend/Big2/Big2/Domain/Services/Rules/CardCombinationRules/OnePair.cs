using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules
{
    public class OnePair : ICombinationRule
    {
        private readonly int _validCount = 2;

        public bool IsValid(List<Card> cards)
        {
            bool includeJoker = cards.Where(c => c.Rank == Rank.Joker).Any();

            return includeJoker || cards[0].Rank == cards[1].Rank;
        }

        public int GetValidCount()
        {
            return _validCount;
        }

        public CardCombinationTypes GetCombinationType()
        {
            return CardCombinationTypes.OnePair;
        }

        public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
        {
            var current = GetRankAndTurnJoker(currCards);
            var compared = GetRankAndTurnJoker(comparedCards);

            if (current.Rank == compared.Rank)
            {
                return current.ReplacedCards.IsSuitGreaterThan(compared.ReplacedCards, current.Rank);
            }
            else
            {
                return current.Rank > compared.Rank;
            }
        }

        private (Rank Rank, List<Card> ReplacedCards) GetRankAndTurnJoker(List<Card> cards)
        {
            List<Rank> mostFrequentRank = cards.GetMostFrequentRank();
            Rank rank;
            List<Card> replacedCards;
            if (mostFrequentRank.Count == 1 && mostFrequentRank[0] == Rank.Joker)
            {
                rank = Rank.Two;
                replacedCards = cards.TurnJokerAs(Card.GetMaxCard());
            }
            else
            {
                rank = mostFrequentRank.Max();
                replacedCards = cards.TurnJokerAs(new Card(Suit.Spades, rank));
            }

            return (rank, replacedCards);
        }

    }
}
