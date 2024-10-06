using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class PlayerBecameNotReady : DomainEvent
    {
        public Player Player { get; set; }

        public PlayerBecameNotReady(Player player)
        {
            this.Player = player;
        }
    }
}
