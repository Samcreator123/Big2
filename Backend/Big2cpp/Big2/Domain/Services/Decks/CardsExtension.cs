namespace Big2.Domain.Services.Decks
{
    public static class CardsExtension
    {
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

        public static List<Card> TurnJokerAs(this List<Card> cards, Card replaceCard)
        {
            Card joker = new(Suit.None, Rank.Joker);

            return cards.Select(originalCard =>
            {
                return originalCard == joker ? replaceCard : originalCard;
            }).ToList();
        }

        public static List<Rank> TurnJokerAs(this List<Rank> ranks, Rank replaceRank)
        {
            return ranks.Select(originalRank =>
            {
                return originalRank == Rank.Joker ? replaceRank : originalRank;
            }).ToList();
        }

        public static int GetMostFrequentCount(this List<Card> cards)
        {
            return cards
                .GroupBy(c => c.Rank)
                .Max(g => g.Count());
        }

        public static bool IsSuitGreaterThan(this List<Card> cards, List<Card> comparedCards, Rank rank)
        {
            var suits = cards.Where(c => c.Rank == rank)
                .OrderByDescending(c => c.Suit)
                .Select(c => c.Suit).ToList();
            var comparedSuits = comparedCards.Where(c => c.Rank == rank)
                .OrderByDescending(c => c.Suit)
                .Select(c => c.Suit).ToList();

            int diff = suits.Count - comparedSuits.Count;

            if (diff > 0)
            {
                comparedSuits.AddRange(Enumerable.Repeat(Suit.None, diff));
            }
            else
            {
                suits.AddRange(Enumerable.Repeat(Suit.None, diff));
            }

            return suits.Zip(comparedSuits, (first, second) => first.CompareTo(second))
                .FirstOrDefault(comparedResult => comparedResult != 0) > 0;
        }
    }
}
