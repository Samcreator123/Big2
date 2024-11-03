namespace Big2.Domain.ValueObjects;

public class ValidHand(ValidHandType ValidhandType, List<Card> cards) : ValueObject
{
    public ValidHandType Type { get; init; } = ValidhandType;

    public List<Card> Cards { get; init; } = cards;

    public bool IsBiggerThan(ValidHand compared)
    {
        return HandRuleHandler.IsFirstGreaterThan(Type, Cards, compared.Type, compared.Cards);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        foreach (var card in Cards) yield return card;
    }
}
