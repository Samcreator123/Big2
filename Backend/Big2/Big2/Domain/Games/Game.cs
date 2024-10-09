using Big2.Domain.Games.Enums;
using Big2.Domain.Games.Events;
using Big2.Domain.Games.Exceptions;
using Big2.Domain.Games.Memetos;
using Big2.Domain.Services.Rules.CardCombinationRules;
using Big2.Helpers.Domain.Event;

namespace Big2.Domain.Games
{
    public class Game
    {
        public Guid ID { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public bool IncludeJoker { get; private set; }

        public int MaxPlayers { get; private set; }

        public bool PlayUntilLast { get; private set; }

        public List<CardCombinationTypes> ValidHands { get; private set; } = [];

        public List<Player> Players { get; private set; } = [];

        public GameState GameState { get; private set; }

        public Guid CreatorID { get; private set; }

        public bool IsEnd { get; private set; }

        private Game() { }

        public static Game Create(string name, bool includeJoker, int maxPlayers,
            bool playUntilLast, List<CardCombinationTypes> validHandtypes)
        {
            if (maxPlayers < 2 || maxPlayers > 4)
            {
                throw new InvalidPlayerCountException("玩家必須在2~4人");
            }

            Game game = new()
            {
                ID = Guid.NewGuid(),
                Name = name,
                IncludeJoker = includeJoker,
                MaxPlayers = maxPlayers,
                PlayUntilLast = playUntilLast,
                ValidHands = validHandtypes,
                Players = new(),
                GameState = GameState.Waiting
            };

            DomainEventPublisher.Raise(new GameCreated(game));

            return game;
        }

        public void JoinGame(Player newPlayer)
        {
            if (Players.Count >= MaxPlayers)
            {
                throw new PlayerLimitExceededException($"玩家已超過該遊戲上限{MaxPlayers}人");
            }

            if (GameState != GameState.Waiting)
            {
                throw new GameInProgressException($"遊戲進行中，請等待下一局");
            }

            if (Players.Count == 0)
            {
                CreatorID = newPlayer.ID;
            }

            Players.Add(newPlayer);

            DomainEventPublisher.Raise(new PlayerJoined(newPlayer));
        }

        public bool CanStart(List<Guid> preparedPlayers)
        {
            var playerIds = this.Players.Select(o => o.ID);
            List<Guid> idNotInGame = preparedPlayers.Where(id => !playerIds.Contains(id)).ToList();

            if (idNotInGame.Count == 0)
            {
                throw new UnknownPlayerIDException($"未知的玩家ID {string.Join(",", idNotInGame)} 於遊戲 {this.ID}");
            }

            return preparedPlayers.Count == playerIds.Count();
        }

        public void SetDealing()
        {
            if (GameState != GameState.Waiting)
            {
                throw new InvalidGameStateException($"只有在遊戲於等待狀態時才能發牌");
            }

            this.GameState = GameState.Dealing;

            DomainEventPublisher.Raise(new GameDealingStarted(this));
        }

        public void SetPlaying()
        {
            if (GameState != GameState.Dealing)
            {
                throw new InvalidGameStateException($"只有在遊戲於發牌狀態時才能開始");
            }

            this.GameState = GameState.Playing;

            DomainEventPublisher.Raise(new GameplayStarted(this));

        }

        public void SetScoring()
        {
            if (GameState != GameState.Playing)
            {
                throw new InvalidGameStateException($"只有在遊戲於進行狀態才能進入發放點數");
            }

            this.GameState = GameState.Scoring;

            DomainEventPublisher.Raise(new ScoringStarted(this));
        }

        public void SetWaiting()
        {
            if (GameState != GameState.Scoring)
            {
                throw new InvalidGameStateException($"只有在遊戲於發放點數狀態才能進入等待狀態");
            }

            this.GameState = GameState.Waiting;

            DomainEventPublisher.Raise(new WaitingStarted(this));
        }

        public void UpdateWhenPlayerEnd(Guid finishedPlayerID)
        {
            var players = this.Players.Where(o => o.ID == finishedPlayerID);

            if (!players.Any())
            {
                throw new UnknownPlayerIDException($"未知的玩家ID {finishedPlayerID} 於遊戲 {this.ID}");
            }

            var player = players.First();

            var finishedPlayers = this.Players.Where(p => p.IsFinished);
            
            player.SetOrder(Players.Count - finishedPlayers.Count() + 1);
            player.SetFinished();

            if (!this.PlayUntilLast || Players.Where(p => !p.IsFinished).Count() == 1)
            {
                this.IsEnd = true;
            }
        }

        public Player GetNextPlayer(Guid playerID)
        {
            var player = this.Players.FirstOrDefault(p => p.ID == playerID) ??
                throw new UnknownPlayerIDException($"未知的玩家ID {playerID} 於遊戲 {this.ID}");

            // 取得當前玩家的索引
            int currentIndex = this.Players.IndexOf(player);

            if (currentIndex == -1)
            {
                throw new UnknownPlayerIDException($"未知的玩家ID {playerID} 於遊戲 {this.ID}");
            }

            // 計算下一個玩家的索引，如果是最後一個玩家，則循環回到第一個玩家
            int nextIndex = (currentIndex + 1) % this.Players.Count;

            var nextPlayer = this.Players[nextIndex];

            return nextPlayer ??
                throw new OnlyOnePlayerException($"未知的玩家ID {playerID} 於遊戲 {this.ID} 沒有下一名玩家了");
        }

        public static Game Restore(GameMemento memento)
        {
            Game game = new(){};

            return game;
        }


    }
}
