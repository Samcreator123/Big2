using Big2.Domain.Players.Exceptions;
using Big2.Domain.Services.Decks;
using Big2.Domain.Services.Rules;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Domain.Players.ValueObjects
{
    public class CurrentCombination
    {

        private readonly Combination? _currentCombination;

        public bool IsNoCombination { get => _currentCombination is null; }

        public bool IsAllPass { get; init; }

        public CardCombinationTypes Type
        {
            get
            {
                if (_currentCombination == null)
                {
                    throw new NullCurrentCombinationException("當前組合為空");
                }
                return _currentCombination.Type;
            }
        }

        public CurrentCombination() { }

        public CurrentCombination(CardCombinationTypes type, List<Card> cards, bool isAllPass)
        {
            _currentCombination = new Combination(type, cards);
            IsAllPass = isAllPass;
        }

        public bool IsWeakerThanPlayCards(List<Card> comparedCards)
        {
            if (_currentCombination == null)
            {
                throw new NullCurrentCombinationException("當前卡牌為空，無法比較");
            }

            bool isMonsterType(CardCombinationTypes type) => CombinationRuleHandler.IsMonsterType(type);

            bool isCombinationEqual(CardCombinationTypes type) => type == _currentCombination.Type;

            var filteredTypes = CombinationRuleHandler
                .GetPossibleCombinationTypes(comparedCards)
                .Where(type => isMonsterType(type) || isCombinationEqual(type));

            if (!filteredTypes.Any())
            {
                throw new UncomparableCombinationException("玩家出的卡牌組合無法比較與當前的卡牌組合比較");
            }

            var compareType = CombinationRuleHandler.GetBestType(comparedCards, this.Type);

            var comparedComination = new Combination(compareType, comparedCards);

            return comparedComination.IsBiggerThan(_currentCombination);
        }


    }
}
