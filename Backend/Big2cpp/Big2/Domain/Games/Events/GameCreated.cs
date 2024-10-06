using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class GameCreated : DomainEvent
    {
        private Game Game { get; set; }

        public GameCreated(Game game) : base()
        { 
            this.Game = game;
        }
    }
}
