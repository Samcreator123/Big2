

namespace Big2.Domain.Entities;
public class Game : Entity
{
    public GameCustomSetting CustomSetting { get; init; }

    public GameState GameState { get; private set; }

    public List<Player> Players { get; private set; } = [];

    public Player CurrentPlayer { 
        get => _currPlayer ?? throw new InvalidGameStateException($"遊戲{Id} 尚未開始，沒有當前玩家"); 
        private set => _currPlayer = value;
    }

    private Player? _currPlayer;

    public bool HasOtherAllPass => Players.Count(p => p.State == PlayerState.Playing) - 1 == Players.Count(p => p.HasPass);

    public bool IsFirstRound => !Players.Any(p => p.CurrentHandNullAllowed != null);

    public Game(Guid gameId, string name, bool includeJoker, int maxPlayers,
        bool playUntilLast, List<ValidHandType> allowedHandTypes)
    {
        if (maxPlayers < 2 || maxPlayers > 4)
        {
            throw new InvalidPlayerCountException("玩家必須在2~4人");
        }

        Id = gameId;
        CustomSetting = new GameCustomSetting(name, includeJoker, maxPlayers, playUntilLast, allowedHandTypes);
        Players = [];
        GameState = GameState.Waiting;
    }

    public Game(GameMemento memento)
    {

    }

    public Guid JoinGameAndGetPlayerId(string playerName)
    {
        if (Players.Count >= CustomSetting.MaxPlayers)
        {
            throw new PlayerLimitExceededException($"玩家已超過該遊戲上限{CustomSetting.MaxPlayers}人");
        }

        if (GameState != GameState.Waiting)
        {
            throw new GameInProgressException($"遊戲進行中，請等待下一局");
        }

        Player player = new Player(playerName, Id);

        Players.Add(player);

        return player.Id;
    }

    public bool CanStart()
    {
        int readyPlayerCount = Players.Count(o => o.State == PlayerState.Ready);

        return readyPlayerCount >= 2 && readyPlayerCount == Players.Count;
    }

    public bool CanEnd()
    {
        int handEmptyPlayerCount = Players.Count(o => o.State == PlayerState.HandEmpty);

        if (CustomSetting.PlayUntilLast)
        {
            return Players.Count - 1 == handEmptyPlayerCount;
        }
        else
        {
            return handEmptyPlayerCount > 0;
        }
    }

    public void SetPlaying()
    {
        if (GameState != GameState.Waiting)
        {
            throw new InvalidGameStateException($"只有在遊戲於等待狀態時才能開始");
        }

        var idAndCards = Deck.ShuffleAndDeal(CustomSetting.IncludeJoker, new(Players.Select(o => o.Id)));

        foreach (var idAndCard in idAndCards)
        {
            var player = GetPlayerById(idAndCard.Key);

            player.SetPlaying(idAndCard.Value);
        }

        GameState = GameState.Playing;

        CurrentPlayer = Players[0];
    }

    public void SetWaiting()
    {
        if (GameState != GameState.Playing)
        {
            throw new InvalidGameStateException($"只有在遊玩時，才能進入等待模式");
        }

        GameState = GameState.Waiting;
    }

    public void LeaveGame(Guid playerId)
    {
        if (!Players.Any(p => p.Id == playerId))
        {
            throw new UnknownPlayerIDException($"玩家 {playerId} 想退出遊戲 {Id}，但他不在遊戲裡");
        }

        Players.RemoveAll(p => p.Id == playerId);
    }

    public Player GetPlayerById(Guid playerId)
    {
        return Players.Where(p => p.Id == playerId).FirstOrDefault() ??
            throw new NotFoundPlayerException($"於遊戲 {Id} 找不到玩家 {playerId}");
    }

    public void ProcessCardPlayAndAddScoreIfPossible(Guid playerId, bool hasPass, List<Card> playCards)
    {
        Player player = GetPlayerById(playerId);

        if (CurrentPlayer.Id == playerId)
        {
            throw new ValidateCurrentPlayerException("不能在自己回合以外出牌");
        }

        if (hasPass)
        {
            player.Pass();
        }
        else
        {
            HandlePlayCardAction(playCards, player);
        }

        SetNextPlayer();
    }

    private void SetNextPlayer()
    {
        if (CurrentPlayer == null)
        {
            throw new InvalidGameStateException($"遊戲 {Id} 尚未開始");
        }

        var currPlayer = Players.FirstOrDefault(p => p.Id == CurrentPlayer.Id)
            ?? throw new NotFoundPlayerException($"於遊戲 {Id} 找不到玩家 {CurrentPlayer.Id}");

        var index = Players.IndexOf(currPlayer);

        if (index == -1)
        {
            throw new NotFoundPlayerException($"當前遊戲 {Id} 有玩家 {CurrentPlayer.Id}，但無法透過索引找到");
        }

        var maxIndex = Players.Count - 1;

        var nextIndex = index + 1 > maxIndex ? 0 : index + 1;

        CurrentPlayer = Players[nextIndex];
    }

    private void HandlePlayCardAction(List<Card> playCards, Player player)
    {
        if (playCards.Count(player.Cards.Contains) != playCards.Count)
        {
            throw new CardNotInHandException("出了不在手牌裡的牌");
        }

        ValidHand playHand = ValidateAndDeterminePlayHand(playCards);

        player.PlayCard(playHand);

        if (player.Cards.Count == 0)
        {
            player.SetHandEmpty();

            var unFinishedPlayesrCard = Players.Where(p => p.State == PlayerState.Playing)
                                               .Select(p => new List<Card>(p.Cards)).ToList();

            player.AddScore(ScoringHandler.CalculateScore(player.CurrentHand, unFinishedPlayesrCard));
        }
    }

    private ValidHand ValidateAndDeterminePlayHand(List<Card> playCards)
    {
        ValidHand playHand;
        if (IsFirstRound)
        {
            if (!FirstCardPlay.IsValid(playCards))
            {
                throw new InvalidFirstCardPlayException("出牌不符合遊戲的第一輪出牌規則");
            }

            playHand = new ValidHand(HandRuleHandler.GetBestType(playCards), playCards);
        }
        else
        {
            ValidHand currentHand = CurrentPlayer.CurrentHand;

            playHand = new ValidHand(HandRuleHandler.GetBestType(playCards, currentHand.Type), playCards);

            if (!HasOtherAllPass && currentHand.IsBiggerThan(playHand))
            {
                throw new PlayedHandTooWeakException("出牌的組合小於現在的牌");
            }
        }

        return playHand;
    }
}
