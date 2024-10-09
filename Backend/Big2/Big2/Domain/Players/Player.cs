using Big2.Domain.Players.Enums;
using Big2.Domain.Players.Events;
using Big2.Domain.Players.Exceptions;
using Big2.Domain.Players.Memetos;
using Big2.Domain.Players.Validators;
using Big2.Domain.Services.Decks;
using Big2.Domain.Services.Rules;
using Big2.Domain.Services.Rules.CardCombinationRules;
using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Players
{
    public class Player
    {
        public Guid GameID { get; private set; }

        public Guid ID { get; private set; } 

        public string Name { get; private set; } = string.Empty;

        public PlayerState State { get; private set; }

        public HashSet<Card> Cards { get; private set; } = [];

        public Combination LastCombination { get; private set; }

        public Guid CurrentPlayerID { get; private set; }

        public CurrentCombination CurrCombination { get; private set; } = new();

        public int Score { get; private set; }

        private bool IsFirstRound { get => CurrCombination.IsNoCombination; }

        public bool IsPassThisRound { get; private set; } = false;

        public bool IsHandEmpty { get => Cards.Count == 0; }

        private Player() { }

        public static Player Create(string name, Guid gameId)
        {
            Player player = new()
            {
                GameID = gameId,
                Name = name,
                ID = Guid.NewGuid(),
                State = PlayerState.NotReady,
                Score = 0,
            };

            return player;
        }

        public void SetReady()
        {
            if (State != PlayerState.NotReady)
            {
                throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.NotReady} 進行準備");
            }

            State = PlayerState.Ready;
            DomainEventPublisher.Raise(new PlayerBecameReady(this));
        }

        public void SetNotReady()
        {
            if (State != PlayerState.Ready)
            {
                throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.Ready} 進行取消準備");
            }

            State = PlayerState.NotReady;
            DomainEventPublisher.Raise(new PlayerBecameNotReady(this));
        }

        public void SetPlaying()
        {
            if (State != PlayerState.Ready)
            {
                throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.Ready} 開始遊戲");
            }

            State = PlayerState.Playing;
            DomainEventPublisher.Raise(new PlayerBecamePlaying(this));
        }

        public void InitialCards(List<Card> cards)
        {
            this.Cards = new HashSet<Card>(cards);
        }

        public void PlayCard(List<Card> currentPlayCards, bool isPass)
        {
            PlayCardValidtor.ValidateCurrentPlayer(this.ID == CurrentPlayerID);

            if (!isPass)
            {
                ValidateCardPlay(currentPlayCards);

                var type = DetermineCombinationType(currentPlayCards);

                this.Cards.RemoveWhere(currentPlayCards.Contains);
                this.CurrCombination = new CurrentCombination(type, currentPlayCards, false);
            }
            else
            {
                this.IsPassThisRound = true;
            }

            DomainEventPublisher.Raise(new CardPlayed(this));

            if (this.IsHandEmpty)
            {
                this.LastCombination = new Combination(CurrCombination.Type, currentPlayCards);
                DomainEventPublisher.Raise(new PlayerHandEmpty(this));
            }
        }

        private void ValidateCardPlay(List<Card> currentPlayCards)
        {
            PlayCardValidtor.ValidateCardInHand(currentPlayCards, IsCardInHand);
            PlayCardValidtor.ValidateCardCombination(currentPlayCards, CombinationRuleHandler.IsValidCombination);

            if (IsFirstRound)
            {
                PlayCardValidtor.ValidateFirstRoundPlay(currentPlayCards, FirstCardPlay.IsValid);
            }
            else if (!CurrCombination.IsAllPass)
            {
                PlayCardValidtor.ValidateCardCombinationStrength(currentPlayCards, CurrCombination.IsWeakerThanPlayCards);
            }
        }

        private CardCombinationTypes DetermineCombinationType(List<Card> currentPlayCards)
        {
            if (IsFirstRound)
            {
                return CombinationRuleHandler.GetBestType(currentPlayCards);
            }

            return CurrCombination.IsAllPass ?
                CombinationRuleHandler.GetBestType(currentPlayCards) :
                CombinationRuleHandler.GetBestType(currentPlayCards, CurrCombination.Type);
        }

        private bool IsCardInHand(List<Card> currentPlayCards)
        {        
            return currentPlayCards.Where(this.Cards.Contains).Count() == currentPlayCards.Count;
        }

        public void AddScore(int score)
        {
            this.Score += score;

            DomainEventPublisher.Raise(new ScoreAdded(this));
        }

        public static Player Restore(PlayerMemento memento)
        {
            Player player = new()
            {
                Name = memento.Name,
                ID = memento.ID,
                State = memento.State,
                Score = memento.Point,
                Cards = memento.Cards,
                CurrentPlayerID = memento.CurrentPlayerID,
            };

            return player;
        }
    }
}
