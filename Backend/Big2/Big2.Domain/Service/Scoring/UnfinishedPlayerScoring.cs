namespace Big2.Domain.Service.Scoring;

public class UnfinishedPlayerScoring
{
    private static readonly int _baseValue = 10;

    public static int Calculate(List<Card> cardLeft)
    {
        int handCardCount = cardLeft.Count;

        return handCardCount * _baseValue * CalculateMultiplier(cardLeft);
    }

    private static int CalculateMultiplier(List<Card> cards)
    {
        var jokerCount = cards.Count(c => c == Card.GetJokerCard());
        var maxCardCount = cards.Count(c => c == Card.GetMaxCard());

        return CalculateMultiplierByCardsCount(cards.Count) *
            CalculateMultiplierByMonsterType(cards) *
            jokerCount *
            maxCardCount;
    }

    private static int CalculateMultiplierByMonsterType(List<Card> cards)
    {
        var hasFourOfAKind = GetAllFiveCards(cards).Any(fiveCard => new FourOfAKind().IsValid(fiveCard));
        var hasStraightFlush = GetAllFiveCards(cards).Any(fiveCard => new StraightFlush().IsValid(fiveCard));


        if (hasFourOfAKind && hasStraightFlush)
        {
            return 4;
        }
        else if (hasFourOfAKind || hasStraightFlush)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private static List<List<Card>> GetAllFiveCards(List<Card> cards)
    {
        return Enumerable.Range(0, cards.Count - 4)
            .Select(i => cards.GetRange(i, 5))
            .ToList();
    }

    private static int CalculateMultiplierByCardsCount(int cardsCount)
    {
        if (cardsCount <= 7)
        {
            return 1;
        }
        else if (cardsCount <= 9)
        {
            return 2;
        }
        else if (cardsCount <= 12)
        {
            return 4;
        }
        else
        {
            return 8;
        }
    }
}
