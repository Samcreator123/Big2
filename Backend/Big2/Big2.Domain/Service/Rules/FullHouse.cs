using Big2.Domain.Extension;

namespace Big2.Domain.Service.Rules;

public class FullHouse : IValidHandRule
{
    // 1 1 2 2 joker、1 1 1 2 joker
    private static readonly List<List<int>> _oneJokerSuitCounts =
        [
            [2,2,1],
            [3,1,1],
        ];

    // 1 1 2 joker joker、 1 1 1 joker joker 
    private static readonly List<List<int>> _twoJokersSuitCounts =
        [
            [2,2,1],
            [3,2],
        ];

    private static readonly int _validCount = 5;

    public int GetValidCount()
    {
        return _validCount;
    }

    public bool IsValid(List<Card> cards)
    {
        var joker = cards.Where(c => c.Rank == Rank.Joker);

        if (joker.Any())
        {
            return IsValidWithJoker(cards, joker.Count());
        }
        else
        {
            var suitCounts = cards.GroupBy(c => c.Rank)
                .Select(g => g.Count())
                .OrderByDescending(count => count);

            return suitCounts.SequenceEqual([3, 2]);
        }
    }

    private bool IsValidWithJoker(List<Card> cards, int jokerNum)
    {
        var suitCounts = cards.GroupBy(c => c.Rank)
                .Select(g => g.Count())
                .OrderByDescending(count => count);

        return jokerNum switch
        {
            1 => _oneJokerSuitCounts.Any(seq => suitCounts.SequenceEqual(seq)),
            2 => _twoJokersSuitCounts.Any(seq => suitCounts.SequenceEqual(seq)),
            _ => false,
        };
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.FullHouse;
    }

    public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
    {
        Rank currentRank = GetRankOfFullHouse(currCards);

        Rank comparedRank = GetRankOfFullHouse(comparedCards);

        if (currentRank == comparedRank)
        {
            var replacedCurr = currCards.TurnJokerAs(new Card(Suit.Spades, currentRank));
            var replacedComparation = comparedCards.TurnJokerAs(new Card(Suit.Spades, comparedRank));

            return replacedCurr.IsSuitGreaterThan(replacedComparation, currentRank);
        }
        else
        {
            return currentRank > comparedRank;
        }
    }

    private Rank GetRankOfFullHouse(List<Card> cards)
    {
        var joker = cards.Where(c => c.Rank == Rank.Joker);

        if (joker.Any())
        {
            if (cards.GetMostFrequentRankCount() >= 3)
            {
                return cards.GetMostFrequentRank()[0];
            }
            else
            {
                return cards.GetMostFrequentRank().Max();
            }
        }
        else
        {
            return cards.GetMostFrequentRank()[0];
        }
    }
}
