using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class ScoringStarted : DomainEvent
    {
        public Game Game { get; private set; }

        public ScoringStarted(Game game)
        {
            Game = game;
        }
    }
}
