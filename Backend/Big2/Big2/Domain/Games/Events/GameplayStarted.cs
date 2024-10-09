using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class GameplayStarted : DomainEvent
    {
        public Game Game { get; private set; }

        public GameplayStarted(Game game)
        {
            Game = game;
        }
    }
}
