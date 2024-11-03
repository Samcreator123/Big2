namespace Big2.Domain.Service.Rules;

public class FirstCardPlay
{
    static readonly Card _firstCard = new(Suit.Clubs, Rank.Three);

    public static bool IsValid(List<Card> cards)
    {
        return cards.Contains(_firstCard) && HandRuleHandler.IsValidHand(cards);
    }
}
