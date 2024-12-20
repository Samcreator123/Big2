﻿using Big2.Domain.Extension;

namespace Big2.Domain.Service.Rules;

public class FourOfAKind : IValidHandRule
{
    // 1 1 1 2 joker、1 1 1 1 joker
    private static readonly List<List<int>> _oneJokerSuitCounts =
        [
            [3,1,1],
            [4,1],
        ];

    // 1 1 2 joker joker、 1 1 1 joker joker 
    private static readonly List<List<int>> _twoJokersSuitCounts =
        [
            [2,2,1],
            [3,2],
        ];

    private static readonly int _validCount = 5;

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

            return suitCounts.SequenceEqual([4, 1]);
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

    public int GetValidCount()
    {
        return _validCount;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.FourOfAKind;
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
