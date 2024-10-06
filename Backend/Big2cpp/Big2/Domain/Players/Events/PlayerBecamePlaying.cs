using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class PlayerBecamePlaying : DomainEvent
    {
        public Player Player { get; set; }

        public PlayerBecamePlaying(Player player)
        {
            this.Player = player;
        }
    }
}
