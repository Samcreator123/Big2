namespace Big2.Domain.Services.Decks
{
    public class Deck
    {
        public List<Card> Cards { get; private set; } = [];

        public bool IncludeJoker { get; private set; }

        private Deck() { }

        public static Deck Create(bool IncludeJoker)
        {
            Deck deck = new()
            {
                IncludeJoker = IncludeJoker
            };

            foreach (var suit in Enum.GetValues(typeof(Rank)))
            {
                if ((Rank)suit != Rank.Joker)
                {
                    foreach (var rank in Enum.GetValues(typeof(Suit)))
                    {
                        deck.Cards.Add(new Card((Suit)rank, (Rank)suit));
                    }
                }
            }

            if (IncludeJoker)
            {
                deck.Cards.Add(new Card(Suit.None, Rank.Joker));
                deck.Cards.Add(new Card(Suit.None, Rank.Joker));
            }

            return deck;
        }

        public void Shuffle()
        {
            Random rng = new(Guid.NewGuid().GetHashCode());
            Cards = Cards.OrderBy(c => rng.Next()).ToList();
        }

        public Dictionary<Guid, List<Card>> DealTheCard(LinkedList<Guid> playerIDs)
        {
            Dictionary<Guid, List<Card>> playerAndCards = new();

            foreach (var playerID in playerIDs)
            {
                playerAndCards.Add(playerID, new());
            }

            var currentPlayer = playerIDs.First;

            foreach (var card in Cards)
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
}
