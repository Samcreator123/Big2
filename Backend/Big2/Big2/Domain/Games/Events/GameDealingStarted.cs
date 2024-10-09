using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class GameDealingStarted : DomainEvent
    {
        public Game Game { get; private set; }

        public GameDealingStarted(Game game)
        {
            Game = game;
        }
    }
}
