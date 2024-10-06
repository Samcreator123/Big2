using Big2.Domain.Players.Exceptions;
using Big2.Domain.Rules.ValueObjects.CardCombinationRules;
using Big2.Domain.Rules.ValueObjects;
using Big2.Domain.Services.Decks;

namespace Big2.Domain.Players.Validators
{
    public class PlayCardValidtor
    {
        public static void ValidateCardInHand(List<Card> cards, Func<List<Card>, bool> rule)
        {
            if (!rule.Invoke(cards))
            {
                throw new CardNotInHandException("出了不在手牌裡的牌");
            }
        }

        public static void ValidateCardCombination(List<Card> cards, Func<List<Card>, bool> rule)
        {
            if (!rule.Invoke(cards))
            {
                throw new InvalidCardCombinationException("無效的卡牌組合");
            }
        }

        public static void ValidateFirstRoundPlay(List<Card> cards, Func<List<Card>, bool> rule)
        {
            if (!rule.Invoke(cards))
            {
                throw new InvalidFirstCardPlayException("出牌不符合遊戲的第一輪出牌規則");
            }
        }

        public static void ValidateCardCombinationStrength(List<Card> cards, Func<List<Card>, bool> rule)
        {
            if (!rule.Invoke(cards))
            {
                throw new PlayedCombinationTooWeakException("出牌的組合小於現在的牌");
            }
        }

        public static void ValidateCurrentPlayer(bool isCurrentPlayer)
        {
            if (!isCurrentPlayer)
            {
                throw new InvalidTurnException("不能在自己回合以外出牌");
            }
        }
    }
}
