using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games.Events
{
    public class ScoreCalculationStarted : DomainEvent
    {
        private Game Game { get; set; }

        public ScoreCalculationStarted(Game game) : base()
        {
            this.Game = game;
        }
    }
}
