namespace Big2.Domain.Services.Decks
{

    public class Card(Suit suit, Rank rank)
    {
        private readonly static Card _maxCard = new Card(Suit.Spades, Rank.Two);

        private readonly static Card _jokerCard = new Card(Suit.None, Rank.Joker);

        public Suit Suit { get; init; } = suit;

        public Rank Rank { get; init; } = rank;

        /// <summary>
        /// 返回非小丑牌的最大花色點數牌
        /// </summary>
        /// <returns>最大花色點數牌</returns>
        public static Card GetMaxCard()
        {
            return _maxCard;
        }

        public static Card GetJokerCard()
        {
            return _jokerCard;
        }

        public int Compare(Card comparedCard)
        {
            if (this.Rank == comparedCard.Rank)
            {
                if (this.Suit == comparedCard.Suit)
                {
                    return 0;
                }
                else 
                {
                    return this.Suit > comparedCard.Suit ? 1 : -1;
                }
            }
            else
            {
                return this.Rank > comparedCard.Rank ? 1 : -1;
            }
        }

        public static bool operator ==(Card card1, Card card2)
        {
            if (ReferenceEquals(card1, card2))
            {
                return true;  // 參考相同
            }

            if (card1 is null || card2 is null)
            {
                return false; // 其中一個為 null
            }

            // 比較卡片屬性
            return card1.Suit == card2.Suit && card1.Rank == card2.Rank;
        }

        // 重載 != 運算子
        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        // 覆寫 Equals 方法
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Card other = (Card)obj;
            return this == other;
        }

        // 覆寫 GetHashCode 方法
        public override int GetHashCode()
        {
            // 使用 Rank 和 Suit 產生唯一的哈希值
            return Rank.GetHashCode() ^ Suit.GetHashCode();
        }
    }
}
