namespace Big2.Domain.Service.Rules;

public class Single : IValidHandRule
{
    private readonly int _validCount = 1;

    public int GetValidCount()
    {
        return _validCount;
    }

    public bool IsValid(List<Card> cards)
    {
        return cards.Count == 1;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.Single;
    }

    public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
    {
        Card maxCard = Card.GetMaxCard();

        Card firstCurr = currCards.TurnJokerAs(maxCard)[0];
        Card firstCompared = comparedCards.TurnJokerAs(maxCard)[0];

        return firstCurr.Compare(firstCompared) > 0;
    }
}
