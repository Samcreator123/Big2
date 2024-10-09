using Big2.Domain.Services.Decks;

namespace Big2.Domain.Services.Rules.CardCombinationRules;

public class CombinationRuleHandler
{
    private readonly static List<ICombinationRule> _validRules = new()
    {
        new StraightFlush() ,
        new FourOfAKind() ,
        new FullHouse() ,
        new Straight() ,
        new ThreeOfAKind() ,
        new TwoPair() ,
        new Triple() ,
        new OnePair() ,
        new Single() ,
    };

    private readonly static Dictionary<CardCombinationTypes, int> _monsterTypesAndOrders = new()
    {
        { CardCombinationTypes.StraightFlush, 1 },
        { CardCombinationTypes.FourOfAKind, 2 },
    };

    public static bool IsValidCombination(List<Card> cards)
    {
        return GetPossibleCombinationTypes(cards).Count != 0;
    }

    /// <summary>
    /// 從可能的卡牌組合獲取與 expectedCombination 相同或同花順、鐵支，順序為
    /// 同花順 > 鐵支 > expectedCombination
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="expectedType"></param>
    /// <returns></returns>
    public static CardCombinationTypes GetBestType(List<Card> cards, CardCombinationTypes expectedType)
    {
        var possibleComTypes = GetPossibleCombinationTypes(cards);

        if (possibleComTypes.Any(type => type == CardCombinationTypes.StraightFlush))
        {
            return CardCombinationTypes.StraightFlush;
        }

        if (possibleComTypes.Any(type => type == CardCombinationTypes.FourOfAKind))
        {
            return CardCombinationTypes.FourOfAKind;
        }

        return expectedType;
    }

    public static CardCombinationTypes GetBestType(List<Card> cards)
    {
        return GetPossibleCombinationTypes(cards).Max();
    }

    public static List<CardCombinationTypes> GetPossibleCombinationTypes(List<Card> cards)
    {
        // 先判斷當前的卡牌符合哪些合法的卡牌組合
        var combinationsForCount = _validRules.Where(com => com.GetValidCount() == cards.Count);

        if (combinationsForCount.Any())
        {
            return combinationsForCount.Where(com => com.IsValid(cards)).Select(rule => rule.GetCombinationType()).ToList();
        }
        else
        {
            return [];
        }
    }

    public static ICombinationRule GetRuleByType(CardCombinationTypes type)
    {
        return _validRules.Where(rule => rule.GetCombinationType() == type).First();
    }

    public static bool IsMonsterType(CardCombinationTypes type)
    {
        return _monsterTypesAndOrders.Keys.Contains(type);
    }

    /// <summary>
    /// 前置條件 : Cards 都符合 Type
    /// </summary>
    /// <param name="firstType"></param>
    /// <param name="firstCards"></param>
    /// <param name="secondType"></param>
    /// <param name="secondCards"></param>
    /// <returns></returns>
    public static bool IsFirstGreaterThan(
        CardCombinationTypes firstType,
        List<Card> firstCards,
        CardCombinationTypes secondType,
        List<Card> secondCards)
    {
        bool isFirstMonster = IsMonsterType(firstType);

        bool isSecondMonster = IsMonsterType(secondType);

        if (isFirstMonster && isSecondMonster)
        {
            return HandleMonsterType(firstType, firstCards, secondType, secondCards);
        }
        else if (isFirstMonster || isSecondMonster)
        {
            return isFirstMonster;
        }
        else
        {
            return GetRuleByType(firstType).IsFirstGreater(firstCards, secondCards);
        }
    }

    private static bool HandleMonsterType(
        CardCombinationTypes firstType,
        List<Card> firstCards,
        CardCombinationTypes secondType,
        List<Card> secondCards)
    {
        if (_monsterTypesAndOrders[firstType] == _monsterTypesAndOrders[secondType])
        {
            ICombinationRule combinationRule = GetRuleByType(firstType);

            return combinationRule.IsFirstGreater(firstCards, secondCards);
        }
        else
        {
            return _monsterTypesAndOrders[firstType] > _monsterTypesAndOrders[secondType];
        }
    }


}
