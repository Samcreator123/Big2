using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class PlayerBecameReady : DomainEvent
    {
        public Player Player { get; set; }

        public PlayerBecameReady(Player player) 
        {
            this.Player = player;
        }
    }
}
