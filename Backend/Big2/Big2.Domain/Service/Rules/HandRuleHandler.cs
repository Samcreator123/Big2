namespace Big2.Domain.Service.Rules;

public class HandRuleHandler
{
    private readonly static List<IValidHandRule> _validRules = new()
    {
        new StraightFlush() ,
        new FourOfAKind() ,
        new FullHouse() ,
        new Straight() ,
        new ThreeOfAKind() ,
        new TwoPair() ,
        new Triple() ,
        new OnePair() ,
        new Rules.Single() ,
    };

    private readonly static Dictionary<ValidHandType, int> _monsterTypesAndOrders = new()
    {
        { ValidHandType.StraightFlush, 1 },
        { ValidHandType.FourOfAKind, 2 },
    };

    public static bool IsValidHand(List<Card> cards)
    {
        return GetPossibleValidHandTypes(cards).Count != 0;
    }

    /// <summary>
    /// 從可能的卡牌組合獲取與 expectedCombination 相同或同花順、鐵支，順序為
    /// 同花順 > 鐵支 > expectedCombination
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="expectedType"></param>
    /// <returns></returns>
    public static ValidHandType GetBestType(List<Card> cards, ValidHandType expectedType)
    {
        var possibleComTypes = GetPossibleValidHandTypes(cards);

        if (possibleComTypes.Any(type => type == ValidHandType.StraightFlush))
        {
            return ValidHandType.StraightFlush;
        }

        if (possibleComTypes.Any(type => type == ValidHandType.FourOfAKind))
        {
            return ValidHandType.FourOfAKind;
        }

        return expectedType;
    }

    public static ValidHandType GetBestType(List<Card> cards)
    {
        var possibleHand = GetPossibleValidHandTypes(cards);

        if(possibleHand.Count == 0)
        {
            throw new InvalidHandException("無效的卡牌組合");
        }

        return possibleHand.Max();
    }

    public static List<ValidHandType> GetPossibleValidHandTypes(List<Card> cards)
    {
        // 先判斷當前的卡牌符合哪些合法的卡牌組合
        var validRulesForCount = _validRules.Where(com => com.GetValidCount() == cards.Count);

        if (validRulesForCount.Any())
        {
            return validRulesForCount.Where(com => com.IsValid(cards))
                .Select(rule => rule.GetCombinationType()).ToList();
        }
        else
        {
            return [];
        }
    }

    public static IValidHandRule GetRuleByType(ValidHandType type)
    {
        return _validRules.Where(rule => rule.GetCombinationType() == type).First();
    }

    public static bool IsMonsterType(ValidHandType type)
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
        ValidHandType firstType,
        List<Card> firstCards,
        ValidHandType secondType,
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
        ValidHandType firstType,
        List<Card> firstCards,
        ValidHandType secondType,
        List<Card> secondCards)
    {
        if (_monsterTypesAndOrders[firstType] == _monsterTypesAndOrders[secondType])
        {
            IValidHandRule validRuleRule = GetRuleByType(firstType);

            return validRuleRule.IsFirstGreater(firstCards, secondCards);
        }
        else
        {
            return _monsterTypesAndOrders[firstType] > _monsterTypesAndOrders[secondType];
        }
    }
}
