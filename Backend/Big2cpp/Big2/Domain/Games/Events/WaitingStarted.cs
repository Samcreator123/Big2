using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class WaitingStarted : DomainEvent
    {
        public Game Game { get; private set; }

        public WaitingStarted(Game game)
        {
            Game = game;
        }
    }
}
