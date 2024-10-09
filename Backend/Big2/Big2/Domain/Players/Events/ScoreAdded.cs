using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class ScoreAdded : DomainEvent
    {
        public Player Player { get; set; }

        public ScoreAdded(Player player)
        {
            this.Player = player;
        }
    }
}
