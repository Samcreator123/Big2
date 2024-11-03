namespace Big2.Domain.Service.Rules;

public interface IValidHandRule
{
    bool IsValid(List<Card> cards);

    int GetValidCount();

    ValidHandType GetCombinationType();

    /// <summary>
    /// 前置條件 : 入參都符合該牌型
    /// </summary>
    /// <param name="currCards"></param>
    /// <param name="comparedCards"></param>
    /// <returns></returns>
    bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards);
}
