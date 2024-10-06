using Big2.Domain.Services.Decks;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Domain.Services.Rules
{
    public class Combination(CardCombinationTypes Combinationtype, List<Card> cards)
    {
        public CardCombinationTypes Type { get; init; } = Combinationtype;

        public List<Card> Cards { get; init; } = cards;

        public bool IsBiggerThan(Combination compared)
        {
            return CombinationRuleHandler.IsFirstGreaterThan(this.Type, this.Cards, compared.Type, compared.Cards);
        }
    }
}
