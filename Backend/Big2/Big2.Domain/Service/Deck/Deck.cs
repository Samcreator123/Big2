namespace Big2.Domain.Service.Deck;
public class Deck
{
    public static Dictionary<Guid, List<Card>> ShuffleAndDeal(bool includeJoker, LinkedList<Guid> playerIDs)
    {
        var cards = GetNewDeck(includeJoker);

        cards = Shuffle(cards);

        return DealTheCard(cards, playerIDs);
    }

    private static List<Card> GetNewDeck(bool includeJoker)
    {
        List<Card> newDeck = [];

        foreach (var suit in Enum.GetValues(typeof(Rank)))
        {
            if ((Rank)suit != Rank.Joker)
            {
                foreach (var rank in Enum.GetValues(typeof(Suit)))
                {
                    newDeck.Add(new Card((Suit)rank, (Rank)suit));
                }
            }
        }

        if (includeJoker)
        {
            newDeck.Add(new Card(Suit.None, Rank.Joker));
            newDeck.Add(new Card(Suit.None, Rank.Joker));
        }

        return newDeck;
    }

    private static List<Card> Shuffle(List<Card> cards)
    {
        Random rng = new(Guid.NewGuid().GetHashCode());

        return cards.OrderBy(c => rng.Next()).ToList();
    }

    private static Dictionary<Guid, List<Card>> DealTheCard(List<Card> cards, LinkedList<Guid> playerIDs)
    {
        Dictionary<Guid, List<Card>> playerAndCards = new();

        foreach (var playerID in playerIDs)
        {
            playerAndCards.Add(playerID, new());
        }

        var currentPlayer = playerIDs.First;

        foreach (var card in cards)
        {
            if (currentPlayer == null)
            {
                break;
            }

            playerAndCards[currentPlayer.Value].Add(card);

            currentPlayer = currentPlayer.Next ?? playerIDs.First;
        }

        return playerAndCards;
    }
}
