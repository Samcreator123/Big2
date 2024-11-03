
namespace Big2.Domain.ValueObjects;

public class Card(Suit suit, Rank rank) : ValueObject
{
    private readonly static Card _maxCard = new(Suit.Spades, Rank.Two);

    private readonly static Card _jokerCard = new(Suit.None, Rank.Joker);

    public Suit Suit { get; init; } = suit;

    public Rank Rank { get; init; } = rank;

    /// <summary>
    /// 返回非小丑牌的最大花色點數牌
    /// </summary>
    /// <returns>最大花色點數牌</returns>
    public static Card GetMaxCard()
    {
        return _maxCard;
    }

    public static Card GetJokerCard()
    {
        return _jokerCard;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Suit;
        yield return Rank;
    }
}
