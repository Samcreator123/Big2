namespace Big2.Domain.Service.Rules;

public class StraightFlush : IValidHandRule
{
    private readonly Straight _straight = new();

    private readonly int _validCount = 5;

    public int GetValidCount()
    {
        return _validCount;
    }

    public ValidHandType GetCombinationType()
    {
        return ValidHandType.StraightFlush;
    }

    public bool IsValid(List<Card> cards)
    {
        if (!IsSameSuit(cards))
        {
            return false;
        }

        return _straight.IsValid(cards);
    }

    private bool IsSameSuit(List<Card> cards)
    {
        return cards
            .GroupBy(o => o.Suit)
            .Max(g => g.Count()) == cards.Count;
    }

    public bool IsFirstGreater(List<Card> currCards, List<Card> comparedCards)
    {
        return _straight.IsFirstGreater(currCards, comparedCards);
    }
}
