namespace Big2.Domain.Service.Rules;

public class StraightRanker
{
    private readonly static List<(List<Rank> sequence, int rank)> _sequenceAndOrders =
        [
            ([Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six], 1),
            ([Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace], 2),
            ([Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King], 3),
            ([Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen], 4),
            ([Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack], 5),
            ([Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten], 6),
            ([Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine], 7),
            ([Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight], 8),
            ([Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven], 9),
            ([Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five], 10)
        ];

    public List<Card> GetStraight(List<Card> cards)
    {
        // 順子排序的等級、順子排序
        Dictionary<int, List<Rank>> orderAndSeq = GetPossibleStraight(cards);

        return orderAndSeq.Count == 0 ? [] : GetMaxRankSequence(cards, orderAndSeq);
    }

    public (int order, List<Card> sequence) GetOrderAndStraight(List<Card> cards)
    {
        // 順子排序的等級、順子排序
        Dictionary<int, List<Rank>> orderAndSeq = GetPossibleStraight(cards);

        return orderAndSeq.Count == 0 ? (99, []) : (orderAndSeq.Keys.Max(), GetMaxRankSequence(cards, orderAndSeq));
    }

    /// <summary>
    /// 獲取所有可能的順子
    /// </summary>
    /// <param name="cards">卡牌</param>
    /// <returns></returns>
    private Dictionary<int, List<Rank>> GetPossibleStraight(List<Card> cards)
    {
        int validCardCount = 5;
        int jokerCount = cards.Where(c => c.Rank == Rank.Joker).Count();

        // 因為 joker 可以做任意牌，所以可以減少符合的數量
        int matchedNum = validCardCount - jokerCount;
        Dictionary<int, List<Rank>> orderAndseq = new();

        var cardRanks = cards.Select(c => c.Rank);
        foreach (var (sequence, order) in _sequenceAndOrders)
        {
            int tmpMatchedNum = sequence.Count(rank => cardRanks.Contains(rank));

            if (tmpMatchedNum >= matchedNum)
            {
                orderAndseq.Add(order, sequence);
            }
        }

        return orderAndseq;
    }

    /// <summary>
    /// 將所有符合的順子可能性，選一個最大的順子
    /// </summary>
    /// <param name="cards">卡牌</param>
    /// <param name="orderAndseq">順子排序的等級以及順子排序</param>
    /// <returns></returns>
    private List<Card> GetMaxRankSequence(List<Card> cards, Dictionary<int, List<Rank>> orderAndseq)
    {
        var cardRanks = cards.Select(c => c.Rank);
        var maxRank = orderAndseq.Keys.Max();
        var maxSequence = orderAndseq[maxRank];

        var missRanks = maxSequence.Where(o => !cardRanks.Contains(o));
        List<Card> tmpCards = new(cards);

        foreach (var rank in missRanks)
        {
            tmpCards.Add(new Card(Suit.Spades, rank));
        }

        // 依照順子排序進行排序
        return maxSequence
            .Select(rank => tmpCards.First(c => c.Rank == rank))
            .ToList();
    }
}
