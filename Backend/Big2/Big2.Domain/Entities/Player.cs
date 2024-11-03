namespace Big2.Domain.Entities;
public class Player : Entity
{
    public Guid GameId { get; init; }

    public string Name { get; private set; } = string.Empty;

    public PlayerState State { get; private set; }

    public HashSet<Card> Cards { get; private set; } = [];


    private ValidHand? _currentHand;

    public ValidHand CurrentHand { 
        get => _currentHand ?? throw new NullCurrentValidHandException($"該玩家 {Id} 還沒有出過牌");
        private set => _currentHand = value;
    } 

    public int Score { get; private set; }

    public bool HasPass { get; set; } = false;

    public Player(string name, Guid gameId)
    {
        Id = Guid.NewGuid();
        Name = name;
        GameId = gameId;
        State = PlayerState.NotReady;
        Score = 0;
    }

    public Player(PlayerMemento memento)
    {

    }

    public void SetReady()
    {
        if (State != PlayerState.NotReady)
        {
            throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.NotReady} 進行準備");
        }

        State = PlayerState.Ready;
    }

    public void SetNotReady()
    {
        State = PlayerState.NotReady;
    }

    public void SetPlaying(List<Card> cards)
    {
        if (State != PlayerState.Ready)
        {
            throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.Ready} 開始遊戲");
        }

        Cards = new(cards);
        State = PlayerState.Playing;
    }

    public void SetHandEmpty()
    {
        if (State != PlayerState.Playing || this.Cards.Count != 0)
        {
            throw new InvalidPlayerStateException($"玩家只能於 {PlayerState.Playing} 並且出完手牌時才能設定為出完手牌");
        }

        State = PlayerState.HandEmpty;
    }

    public void Pass()
    {
        HasPass = true;
    }

    public void PlayCard(ValidHand hand)
    {
        HasPass = false;

        Cards.RemoveWhere(hand.Cards.Contains);

        CurrentHand = hand;
    }

    public void AddScore(int score)
    {
        Score += score;
    }
}
