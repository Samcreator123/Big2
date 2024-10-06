using Big2.Domain.Services.Decks;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Domain.Services.Rules
{
    public class FirstCardPlay
    {
        static readonly Card _firstCard = new(Suit.Clubs, Rank.Three);

        public static bool IsValid(List<Card> cards)
        {
            return cards.Contains(_firstCard) && CombinationRuleHandler.IsValidCombination(cards);
        }
    }
}
