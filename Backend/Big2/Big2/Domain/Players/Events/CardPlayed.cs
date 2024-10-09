using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players.Events
{
    public class CardPlayed : DomainEvent
    {
        public Player Player { get; set; }

        public CardPlayed(Player player)
        {
            this.Player = player;
        }
    }
}
