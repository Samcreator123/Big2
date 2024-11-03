namespace Big2.Domain.Service.Rules;

public class Triple : IValidHandRule
{
    private readonly int _validCount = 3;

    public int GetValidCount()
    {
        return _validCount;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.Triple;
    }

    public bool IsValid(List<Card> cards)
    {
        bool includeJoker = cards.Where(c => c.Rank == Rank.Joker).Any();

        if (includeJoker)
        {
            return IsValidWithJoker(cards);
        }
        else
        {
            return cards.GroupBy(o => o.Rank).Count() == 1;
        }
    }

    private bool IsValidWithJoker(List<Card> cards)
    {
        // 在含有 joker 的情況下，兩種牌就可以了
        return cards.Select(card => card.Rank).Distinct().Count() <= 2;
    }

    public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
    {
        var replacedCurr = ReplaceJokers(currCards);
        var replaceCompared = ReplaceJokers(comparedCards);

        Rank currentRank = replacedCurr.GetMostFrequentRank()[0];
        Rank comparedRank = replaceCompared.GetMostFrequentRank()[0];

        if (currentRank == comparedRank)
        {
            return currCards.IsSuitGreaterThan(replaceCompared, currentRank);
        }
        else
        {
            return currentRank > comparedRank;
        }
    }

    private List<Card> ReplaceJokers(List<Card> cards)
    {
        var jokerCount = cards.Count(c => c.Rank == Rank.Joker);

        if (jokerCount == 0)
        {
            return cards;
        }

        if (jokerCount == 1)
        {
            Rank maxRank = cards.GroupBy(c => c.Rank)
                .Where(g => g.Count() >= 2)
                .Select(g => g.Key)
                .Max();

            return cards.TurnJokerAs(new Card(Suit.Spades, maxRank));
        }
        else
        {
            Rank maxRank = cards.Select(c => c.Rank)
                .Max();

            return cards.TurnJokerAs(new Card(Suit.Spades, maxRank));
        }
    }
}
