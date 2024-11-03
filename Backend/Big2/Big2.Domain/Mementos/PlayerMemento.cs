namespace Big2.Domain.Mementos;

public class PlayerMemento
{
    public Guid ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public PlayerState State { get; set; }

    public int Point { get; set; }

    public HashSet<Card> Cards { get; set; } = new();

    public Guid CurrentPlayerID { get; set; }
}
