namespace Big2.Domain.Service.Rules;

public class TwoPair : IValidHandRule
{
    private readonly int _validCount = 5;

    public int GetValidCount()
    {
        return _validCount;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.TwoPair;
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
            var suitCounts = cards.GroupBy(o => o.Rank)
                                  .Select(g => g.Count())
                                  .OrderByDescending(count => count)
                                  .ToList();

            return suitCounts.SequenceEqual(new List<int> { 3, 2 }) ||
                    suitCounts.SequenceEqual(new List<int> { 2, 2, 1 });
        }
    }

    /// <summary>
    /// 以下可能性，不能低於三組、不能高於四組
    /// 1 2 3 3 joker => 四組
    /// 1 1 2 2 joker => 三組
    /// 1 1 1 2 joker => 三組
    /// 1 2 3 joker joker => 四組
    /// 1 1 2 joker joker => 三組
    /// 1 1 1 joker joker => 兩組
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    private bool IsValidWithJoker(List<Card> cards, int jokerNum)
    {
        var suitCounts = cards.GroupBy(o => o.Rank)
                  .Select(g => g.Count())
                  .OrderByDescending(count => count);

        var validSequences = new List<List<int>>
        {
            new() { 2, 1, 1, 1 },
            new() { 2, 2, 1 },
            jokerNum == 2 ? new() { 3, 2 } : new() { 3, 1, 1 }
        };

        return validSequences.Any(seq => suitCounts.SequenceEqual(seq));
    }

    public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
    {
        var currPairs = GetPairs(ReplaceJokers(currCards));
        var comparedPairs = GetPairs(ReplaceJokers(comparedCards));

        bool isfirstPairEquals = GetComparationList(currPairs.firstPair, comparedPairs.firstPair)
            .Count(result => result == 0) == 2;


        if (isfirstPairEquals)
        {
            return GetComparationList(currPairs.secondPair, comparedPairs.secondPair)
                .FirstOrDefault(result => result != 0) > 0;
        }
        else
        {
            return GetComparationList(currPairs.firstPair, comparedPairs.firstPair)
                .FirstOrDefault(result => result != 0) > 0;
        }
    }

    private List<Card> ReplaceJokers(List<Card> cards)
    {
        var jokerCount = cards.Count(c => c.Rank == Rank.Joker);

        return jokerCount switch
        {
            0 => cards,
            1 => HandleSingleJoker(cards),
            2 => HandleTwoJokers(cards),
            3 => HandleThreeJokers(cards),
            _ => cards.TurnJokerAs(Card.GetMaxCard()),
        };
    }

    private List<Card> HandleSingleJoker(List<Card> cards)
    {
        bool hasAlreadyTwoPair = cards.GroupBy(c => c.Rank)
            .Count(g => g.Count() >= 2) == 2;

        if (hasAlreadyTwoPair)
        {
            return cards.TurnJokerAs(Card.GetMaxCard());
        }
        else
        {
            Rank lastMaxRank = GetLastMaxRank(cards);

            return cards.TurnJokerAs(new Card(Suit.Spades, lastMaxRank));
        }
    }

    private List<Card> HandleTwoJokers(List<Card> cards)
    {
        bool hasAlreadyOnePair = cards.Where(c => c.Rank != Rank.Joker)
            .GroupBy(c => c.Rank)
            .Count(g => g.Count() >= 2) == 1;

        if (hasAlreadyOnePair)
        {
            return cards.TurnJokerAs(Card.GetMaxCard());
        }
        else
        {
            var lastTwoLargestRank = cards.Where(c => c.Rank != Rank.Joker)
                .OrderByDescending(c => c.Rank)
                .Take(2);

            return cards.Select(card =>
            {
                var isReplacedCard = lastTwoLargestRank.Any(beReplaced => beReplaced == card);

                return isReplacedCard ? new Card(Suit.Spades, card.Rank) : card;
            }).ToList();
        }
    }

    private List<Card> HandleThreeJokers(List<Card> cards)
    {
        Rank lastMaxRank = GetLastMaxRank(cards);

        List<Card> cardsWithoutJokers = cards.Where(c => c.Rank != Rank.Joker).ToList();

        cardsWithoutJokers.AddRange(Enumerable.Repeat(Card.GetMaxCard(), 2));
        cardsWithoutJokers.Add(new Card(Suit.Spades, lastMaxRank));

        return cardsWithoutJokers;
    }

    private Rank GetLastMaxRank(List<Card> cards)
    {
        return cards.Where(c => c.Rank != Rank.Joker)
            .GroupBy(c => c.Rank)
            .Where(g => g.Count() == 1)
            .Select(g => g.Key)
            .Max();
    }

    private (List<Card> firstPair, List<Card> secondPair) GetPairs(List<Card> cards)
    {
        var groupByRank = cards.GroupBy(c => c.Rank)
            .Where(g => g.Count() >= 2)
            .Select(g => g.ToList())
            .ToList();

        if (groupByRank.Count == 1)
        {
            var list = groupByRank.First();

            return ([list[0], list[1]], [list[2], list[3]]);
        }
        else if (groupByRank.Count == 2)
        {
            var firstPair = groupByRank[0];
            var secondPair = groupByRank[1];

            return ([firstPair[0], firstPair[1]], [secondPair[0], secondPair[1]]);

        }

        return ([], []);
    }

    public IEnumerable<int> GetComparationList(List<Card> firstPair, List<Card> secondPair)
    {
        return firstPair
            .Zip(secondPair, (curr, compared) => curr.Compare(compared));
    }
}
