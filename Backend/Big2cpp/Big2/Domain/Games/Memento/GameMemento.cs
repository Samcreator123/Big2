using Big2.Domain.Games.Enums;
using Big2.Domain.Services.Rules.CardCombinationRules;

namespace Big2.Domain.Games.Memetos
{
    public class GameMemento
    {
        public Guid ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IncludeJoker { get; set; }

        public int MaxPlayers { get; set; }

        public bool PlayUntilLast { get; set; }

        public List<CardCombinationTypes> ValidHands { get; set; } = new();

        public LinkedList<Player> PlayerIDs { get; set; } = new();

        public GameState GameState { get; set; }

        public Guid CreatorID { get; set; }
    }
}
