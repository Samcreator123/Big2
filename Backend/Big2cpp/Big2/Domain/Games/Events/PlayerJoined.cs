using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class PlayerJoined : DomainEvent
    {
        public Player Player { get; set; }

        public PlayerJoined(Player player)
        {
            Player = player;
        }
    }
}
