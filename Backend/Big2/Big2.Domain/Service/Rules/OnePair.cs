namespace Big2.Domain.Service.Rules;

public class OnePair : IValidHandRule
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

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.OnePair;
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

        if (mostFrequentRank.Count == 1 && mostFrequentRank[0] == Rank.Joker)
        {
            return (Rank.Two, cards.TurnJokerAs(Card.GetMaxCard()));
        }
        else
        {
            Rank rank = mostFrequentRank.Max();

            return (rank, cards.TurnJokerAs(new Card(Suit.Spades, rank)));
        }
    }

}
