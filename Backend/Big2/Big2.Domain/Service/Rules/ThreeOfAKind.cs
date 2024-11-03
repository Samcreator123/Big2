namespace Big2.Domain.Service.Rules;

public class ThreeOfAKind : IValidHandRule
{
    // 1 1 2 3 joker、1 1 2 2 joker、1 1 1 2 joker、1 1 1 1 joker
    private static readonly List<List<int>> _oneJokerSuitCounts =
        [
            [ 2, 1, 1, 1],
            [ 2, 2, 1],
            [ 3, 1, 1],
            [ 4, 1]
        ];

    // 1 2 3 joker joker、1 1 2 joker joker、1 1 1 joker joker
    private static readonly List<List<int>> _twoJokersSuitCounts =
        [
            [ 2, 1, 1, 1],
            [ 2, 2, 1],
            [ 3, 2],
        ];

    private readonly int _validCount = 5;

    public int GetValidCount()
    {
        return _validCount;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.ThreeOfAKind;
    }

    public bool IsValid(List<Card> cards)
    {
        var jokers = cards.Where(c => c.Rank == Rank.Joker);

        if (jokers.Any())
        {
            return IsValidWithJoker(cards, jokers.Count());
        }
        else
        {
            var sameSuitGroup = cards.GroupBy(card => card.Rank)
                .Select(group => group.Count())
                .OrderByDescending(cnt => cnt).ToList();

            return sameSuitGroup[0] >= 3;
        }
    }

    private bool IsValidWithJoker(List<Card> cards, int jokerNum)
    {
        var suitCounts = cards.GroupBy(c => c.Rank)
            .Select(group => group.Count()).OrderByDescending(o => o);

        return jokerNum switch
        {
            1 => _oneJokerSuitCounts.Any(seq => suitCounts.SequenceEqual(seq)),
            2 => _twoJokersSuitCounts.Any(seq => suitCounts.SequenceEqual(seq)),
            _ => false,
        };
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
            Rank maxRank = cards.Select(c => c.Rank).Max();
            return cards.TurnJokerAs(new Card(Suit.Spades, maxRank));
        }
    }
}
