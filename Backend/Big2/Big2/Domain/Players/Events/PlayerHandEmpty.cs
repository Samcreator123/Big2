using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class PlayerHandEmpty : DomainEvent
    {
        public Player Player { get; set; }

        public PlayerHandEmpty(Player player)
        {
            this.Player = player;
        }
    }
}
