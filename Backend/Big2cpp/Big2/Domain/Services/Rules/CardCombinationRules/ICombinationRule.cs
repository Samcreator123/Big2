using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules
{
    public interface ICombinationRule
    {
        bool IsValid(List<Card> cards);

        int GetValidCount();

        CardCombinationTypes GetCombinationType();

        /// <summary>
        /// 前置條件 : 入參都符合該牌型
        /// </summary>
        /// <param name="currCards"></param>
        /// <param name="comparedCards"></param>
        /// <returns></returns>
        bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards);
    }
}
