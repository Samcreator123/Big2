namespace Big2.Domain.ValueObjects;
public static class Big2CardExtension
{
    /// <summary>
    /// 比較大老二中，比較卡牌與被比較卡牌的大小.
    /// </summary>
    /// <param name="card">卡牌</param>
    /// <param name="comparedCard">被比較的卡牌</param>
    /// <returns>若卡牌比較大返回 1, 相等為 0，小於為 -1</returns>
    public static int Compare(this Card card, Card comparedCard)
    {
        int cardRank = GetBigTwoRank(card.Rank);
        int comparedRank = GetBigTwoRank(comparedCard.Rank);

        if (cardRank == comparedRank)
        {
            if (card.Suit == comparedCard.Suit)
            {
                return 0;
            }
            else
            {
                return card.Suit > comparedCard.Suit ? 1 : -1;
            }
        }
        else
        {
            return cardRank > comparedRank ? 1 : -1;
        }
    }

    /// <summary>
    /// 將卡牌的點數大小轉換成大老二的版本
    /// </summary>
    /// <param name="rank"></param>
    /// <returns></returns>
    private static int GetBigTwoRank(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => 12,
            Rank.Two => 13,
            _ => (int)rank - 2,
        };
    }

    /// <summary>
    /// 獲取出現頻率最高的牌
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static List<Rank> GetMostFrequentRank(this List<Card> cards)
    {
        if (cards.Count == 0)
        {
            return [];
        }

        var groups = cards.GroupBy(c => c.Rank)
                    .OrderByDescending(g => g.Count());

        if (groups.Any())
        {
            return groups.Where(g => g.Count() == groups.First().Count())
                    .Select(g => g.Key).ToList();
        }
        else
        {
            return [];
        }
    }

    /// <summary>
    /// 將 Joker 轉為成另一張卡
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="replaceCard">另一張卡</param>
    /// <returns></returns>
    public static List<Card> TurnJokerAs(this List<Card> cards, Card replaceCard)
    {
        Card joker = new(Suit.None, Rank.Joker);

        return cards.Select(originalCard =>
        {
            return originalCard == joker ? replaceCard : originalCard;
        }).ToList();
    }

    /// <summary>
    /// 將 Joker 點數轉為另一個點數
    /// </summary>
    /// <param name="ranks">點數</param>
    /// <param name="replaceRank">另一個點數</param>
    /// <returns></returns>
    public static List<Rank> TurnJokerAs(this List<Rank> ranks, Rank replaceRank)
    {
        return ranks.Select(originalRank =>
        {
            return originalRank == Rank.Joker ? replaceRank : originalRank;
        }).ToList();
    }

    /// <summary>
    /// 獲取手牌中點數最多的卡片有幾個.
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <returns></returns>
    public static int GetMostFrequentRankCount(this List<Card> cards)
    {
        return cards
            .GroupBy(c => c.Rank)
            .Max(g => g.Count());
    }

    /// <summary>
    /// 比較手牌跟要比較的手牌，兩對手牌某點數的花色，手牌是否比要比較的手牌還要大.
    /// </summary>
    /// <param name="cards">手牌.</param>
    /// <param name="comparedCards">要比較的手牌.</param>
    /// <param name="rank">指定的點數</param>
    /// <returns></returns>
    public static bool IsSuitGreaterThan(this List<Card> cards, List<Card> comparedCards, Rank rank)
    {
        // 獲取花色
        var suits = cards.Where(c => c.Rank == rank)
            .OrderByDescending(c => c.Suit)
            .Select(c => c.Suit).ToList();
        var comparedSuits = comparedCards.Where(c => c.Rank == rank)
            .OrderByDescending(c => c.Suit)
            .Select(c => c.Suit).ToList();

        // 兩對手牌某個點數的卡片數量差異
        int diff = suits.Count - comparedSuits.Count;

        // 填充卡片
        if (diff > 0)
        {
            comparedSuits.AddRange(Enumerable.Repeat(Suit.None, diff));
        }
        else
        {
            suits.AddRange(Enumerable.Repeat(Suit.None, diff));
        }

        // 判斷手牌是否要比較的手牌還要大
        return suits.Zip(comparedSuits, (first, second) => first.CompareTo(second))
            .FirstOrDefault(comparedResult => comparedResult != 0) > 0;
    }
}
